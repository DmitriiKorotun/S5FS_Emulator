using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FileCleaner
    {
        public static void DelOneFile(FileDataStorage storage, int blockSize, int nodeNum, int dirNum, short uid, short gid)
        {
            //if (DiagTools.IsFileLocked(new System.IO.FileInfo("FS"), false))
            //{
            //    throw new System.IO.IOException();
            //}
            Thread.Sleep(200);
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open,
System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {

                var node = DataExtractor.GetINode(fs, nodeNum);
                if (node.Flag.System)
                    throw new OSException.SystemDeleteException("Вы пытаетесь удалить системный файл");
                if (DirSeeker.IfCan(node, SystemSigns.Signs.WRITE, uid, gid))
                {
                    var bm = SetFileBlocksFree(fs, node, storage.Bitmap.BitmapValue, storage.Superblock.ClusterSize);
                    fs.Position = (long)OffsetHandbook.posGuide.BITMAP;
                    FSPartsWriter.WriteBitmap(fs, bm, storage.Superblock.ClusterSize);

                    var iMap = SetNodeFree(fs, storage.INodeMap.BitmapValue, nodeNum);
                    fs.Position = (long)OffsetHandbook.posGuide.IMAP;
                    FSPartsWriter.WriteBitmap(fs, iMap, storage.Superblock.ClusterSize);

                    //ByteWriter.WriteBlock(fs, (long)OffsetHandbook.posGuide.BITMAP, blockSize, bm);
                    ClearFolderFromFile(fs, blockSize, (short)dirNum, (short)nodeNum);
                }
                else
                    throw new OSException.AccessException("У вас недостаточно прав для этой операции");
            }
        }

        private static void ClearFolderFromFile(System.IO.FileStream fs, int blockSize, short dirNum, short fileNum)
        {
            var blockNum = DirSeeker.GetFileDirBlockNum(fs, blockSize, dirNum, fileNum);
            var block = BlocksHandler.GetBlock(fs, blockSize, (short)blockNum);
            var offset = DirSeeker.GetFileDirOffset(block, fileNum);
            ByteWriter.WriteBlock(fs, (long)OffsetHandbook.posGuide.MAINDIR + (blockNum - 1) *
                blockSize ,blockSize, DelFileFolderBytes(block, offset));
        }

        private static byte[] DelFileFolderBytes(byte[] block, int offset)
        {
            var junk = new byte[(int)OffsetHandbook.sizeGuide.FILEINDIR];
            junk.CopyTo(block, offset);
            return block;
        }


        private static void DelFileInFolder(FileDataStorage storage, System.IO.FileStream fs, int dirNum, int nodeNum,
            short uid, short gid)
        {
            var node = DataExtractor.GetINode(fs, nodeNum);
            if (node.Flag.System)
                throw new OSException.SystemDeleteException("Вы пытаетесь удалить системный файл");
            if (DirSeeker.IfCan(node, SystemSigns.Signs.WRITE, uid, gid))
            {
                var bm = SetFileBlocksFree(fs, node, storage.Bitmap.BitmapValue, storage.Superblock.ClusterSize);
                FSPartsWriter.WriteBitmap(fs, bm, storage.Superblock.ClusterSize);
                ClearFolderFromFile(fs, 4096, (short)dirNum, (short)nodeNum);
            }
            else
                throw new OSException.AccessException("У вас недостаточно прав для этой операции");
        }

        public static void DelDir(FileDataStorage storage, int nodeNum, int dirNum, short uid, short gid)
        {
            //if (DiagTools.IsFileLocked(new System.IO.FileInfo("FS"), false))
            //{
            //    throw new System.IO.IOException();
            //}
            Thread.Sleep(200);
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open,
System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                var node = DataExtractor.GetINode(fs, nodeNum);
                if (node.Flag.System)
                    throw new OSException.SystemDeleteException("Вы пытаетесь удалить системный файл");
                if (DirSeeker.IfCan(node, SystemSigns.Signs.WRITE, uid, gid))
                {
                    for (int i = 0; i < node.Di_addr.Length; ++i)
                    {
                        if (node.Di_addr[i] != 0)
                        {
                            var block = BlocksHandler.GetBlock(fs, storage.Superblock.ClusterSize, node.Di_addr[i]);
                            for (int j = 16; j < block.Length;
                                j += OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR))
                            {
                                var fileNodeNum = BitConverter.ToInt16(block, j);
                                var isDir = BitConverter.ToBoolean(block, j + 2);
                                if (fileNodeNum == 0)
                                    continue;
                                if (isDir)
                                    DelDir(storage, nodeNum, fileNodeNum, uid, gid);
                                else
                                    DelFileInFolder(storage, fs, nodeNum, fileNodeNum, uid, gid);
                            }
                        }
                    }
                    var bm = SetDirBlocksFree(fs, node.Di_addr, storage.Bitmap.BitmapValue);
                    fs.Position = (long)OffsetHandbook.posGuide.BITMAP;
                    FSPartsWriter.WriteBitmap(fs, bm, storage.Superblock.ClusterSize);

                    var iMap = SetNodeFree(fs, storage.INodeMap.BitmapValue, nodeNum);
                    fs.Position = (long)OffsetHandbook.posGuide.IMAP;
                    FSPartsWriter.WriteBitmap(fs, iMap, storage.Superblock.ClusterSize);
                    ClearFolderFromFile(fs, 4096, (short)dirNum, (short)nodeNum);
                }
                else
                    throw new OSException.AccessException("У вас недостаточно прав для этой операции");
            }
        }

        private static byte[] SetFileBlocksFree(System.IO.FileStream fs, INode node, byte[] bitmap, int blockSize)
        {
            for (int i = 0; i < node.Di_addr.Length - 1; ++i)
            {
                if (node.Di_addr[i] != 0)
                    BitWorker.TurnBitOff(bitmap, node.Di_addr[i]);
            }
            if (BlocksHandler.IsBlocksMany(node.Size, blockSize))
            {
                if (node.Di_addr[node.Di_addr.Length - 1] != 0)
                {
                    var addrBlock = BlocksHandler.GetBlock(fs, blockSize, node.Di_addr[node.Di_addr.Length - 1]);
                    for (int i = 0; i < addrBlock.Length; i += 2)
                    {
                        var addr = ByteConverter.ShortFromBytes(addrBlock, i);
                        if (addr != 0)
                            BitWorker.TurnBitOff(bitmap, addr);
                    }
                }
            }
            else if (node.Di_addr[node.Di_addr.Length - 1] != 0)
                BitWorker.TurnBitOff(bitmap, node.Di_addr[node.Di_addr.Length - 1]);
            return bitmap;
        }

        private static byte[] SetDirBlocksFree(System.IO.FileStream fs, short[] di_addr, byte[] bitmap)
        {
            for (var i = 0; i < di_addr.Length; ++i)
            {
                if (di_addr[i] == 0)
                    continue;
                BitWorker.TurnBitOff(bitmap, di_addr[i]);
            }
            return bitmap;
        }

        private static byte[] SetNodeFree(System.IO.FileStream fs, byte[] nodeMap, int nodeNum)
        {
            BitWorker.TurnBitOff(nodeMap, nodeNum);
            return nodeMap;
        }
    }
}
