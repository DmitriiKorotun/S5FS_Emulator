using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DirHandler
    {
        public static void WriteDir(INode iNode, string path, short dirNode)
        {
            var storage = DataExtractor.GetData(path);
            var nodeNum = PrepDirData(ref iNode, ref storage, path);
            WriteDir(System.IO.File.OpenWrite(path), iNode, storage, nodeNum);
            if (dirNode != (short)SystemSigns.Signs.CREATEMAINDIR)
                DirHandler.AddFileToDir(iNode.Name, storage.Superblock.ClusterSize, dirNode,
                nodeNum, iNode.Flag.Type);
        }

        private static short PrepDirData(ref INode iNode, ref FileDataStorage storage, string path)
        {
            int nodeNum = DataExtractor.GetINodeNum(storage.INodeMap);
            var blocks = BlocksHandler.GetBlocksArr(storage.Bitmap, iNode.Size, storage.Superblock.ClusterSize);
            for (int i = 0; i < blocks.Length; ++i)
            {
                storage.Bitmap.BitmapValue = BitWorker.TurnBitOn(storage.Bitmap.BitmapValue, blocks[i]);
            }
            storage.INodeMap.BitmapValue = BitWorker.TurnBitOn(storage.INodeMap.BitmapValue, nodeNum);

            var dataDict = BlocksHandler.GetDataArr(blocks, new byte[] { 0 }, storage.Superblock.ClusterSize);
            List<int> dataKeys = new List<int>(dataDict.Keys);
            for (int index = 0; index < iNode.Di_addr.Length && index < dataDict.Count; ++index)
            {
                iNode.Di_addr[index] = (short)dataKeys[index]; //To change short for int cause data can be lost
            }

            return (short)nodeNum;
        }


        private static void WriteDir(System.IO.FileStream fs, INode iNode, FileDataStorage storage, int nodeNum)
        {
            using (fs)
            {
                fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.BITMAP);
                FSPartsWriter.WriteBitmap(fs, storage.Bitmap, storage.Superblock.ClusterSize);
                FSPartsWriter.WriteBitmap(fs, storage.INodeMap, storage.Superblock.ClusterSize);
                fs.Position += INode.Offset * (nodeNum - 1);
                FSPartsWriter.WriteINode(fs, iNode);
            }
        }



        public static void AddFileToDir(string name, int blockSize, short dirNode, short fileNode, bool type)
        {
            PrepFileToDir(name, blockSize, dirNode, fileNode, type);
        }

        private static void PrepFileToDir(string name, int blockSize, short dirNode, short fileNode, bool type)
        {
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open))
            {
                var node = DataExtractor.GetINode(fs, dirNode);
                try
                {
                    var prepInfo = GetFileInfo(fs, ComposeInfo(name, fileNode, type), blockSize, dirNode);
                    if (!WriteBlock(fs, prepInfo, blockSize, GetFreeBlock(fs, node.Di_addr, blockSize)))
                        throw new System.IO.IOException("File dir data hasn't been written in the file");
                }
                catch (OSException.DirBlocksException)
                {
                    try
                    {
                        var blockNum = SetNewDirBlock(fs, ref node, blockSize, dirNode);
                        var prepInfo = GetFileInfo(fs, ComposeInfo(name, fileNode, type), blockSize, dirNode);
                        if (!WriteBlock(fs, prepInfo, blockSize, blockNum))
                            throw new System.IO.IOException("File dir data hasn't been written in the file");
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        private static byte[] GetFileInfo(System.IO.FileStream fs, byte[] data, int blockSize, int dNodeNum)
        {
            var dirNode = DataExtractor.GetINode(fs, dNodeNum);
            var blockNum = GetFreeBlock(fs, dirNode.Di_addr, blockSize);
            var block = BlocksHandler.GetBlock(fs, blockSize, (short)blockNum);
            return GetFilledBlock(block, data, GetOffset(block));
        }

        private static int GetFreeBlock(System.IO.FileStream fs, short[] di_addr, int blockSize)
        {
            var blockNum = -1;
            for (int i = 0; i < di_addr.Length; ++i)
            {
                if (di_addr[i] == 0)
                {
                    if (i == di_addr.Length - 1)
                        throw new OSException.DirBlocksException("Dir hasn't free blocks");
                    continue;
                }
                var block = BlocksHandler.GetBlock(fs, blockSize, di_addr[i]);

                if (GetOffset(block) > -1)
                {
                    blockNum = di_addr[i];
                    break;
                }
                else if (i == di_addr.Length - 1)
                    throw new OSException.DirBlocksException("Dir hasn't free blocks");
            }
            return blockNum;
        }

        private static int GetOffset(byte[] block)
        {
            var offset = -1;
            for (int i = 0; i < block.Length / OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR); ++i)
            {
                bool isFree = true;
                for (int j = 0; j < OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR); ++j)
                {
                    if (block[j + i * OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR)] != 0)
                    {
                        isFree = false;
                        break;
                    }
                }
                if (isFree)
                {
                    offset = i * OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR);
                    break;
                }
            }
            return offset;
        }

        private static byte[] GetFilledBlock(byte[] block, byte[] data, int startIndex)
        {
            if (startIndex < 0)
                throw new OSException.BlockSpaceException("Current block hasn't free space");
            if (data.Length != OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR))
                throw new ArgumentException("Data size isn't correct");
            data.CopyTo(block, startIndex);
            return block;
        }

        private static byte[] ComposeInfo(string name, short nodeNum, bool type)
        {
            var data = new byte[OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR)];
            byte[] bName = Encoding.ASCII.GetBytes(name);
            if (bName.Length > 16)
                throw new OverflowException("FileName is too long");
            bName.CopyTo(data, 0);
            BitConverter.GetBytes(nodeNum).CopyTo(data, 16);
            BitConverter.GetBytes(type).CopyTo(data, 18);
            return data;
        }

        private static bool WriteBlock(System.IO.FileStream fs, byte[] data, int blockSize, int blockNum)
        {
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (blockNum - 1) * blockSize;
            ByteWriter.WriteBlock(fs, blockSize, data);
            return true;
        }

        private static bool WriteBitmap(System.IO.FileStream fs, int blockNum, int blockSize)
        {
            var bitmap = DataExtractor.GetBitmap(fs, 1920);
            BitWorker.TurnBitOff(bitmap, blockNum);
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.BITMAP);
            FSPartsWriter.WriteBitmap(fs, bitmap, blockSize);
            return true;
        }

        public static short SetNewDirBlock(System.IO.FileStream fs, ref INode node, int blockSize, short nodeNum)
        {
            var blockNum = AppendBlockToDir(ref node, DataExtractor.GetBitmap(fs, 1920));
            if(!WriteBlockToDir(fs, blockNum, blockSize) || !WriteNodeToFile(fs, node, nodeNum))
                throw new Exception();
            return blockNum;
        }

        public static short AppendBlockToDir(ref INode dirNode, byte[] bitmap)
        {
            short blockNum = -1;
            for (int i = 0; i < dirNode.Di_addr.Length; ++i)
            {
                if (dirNode.Di_addr[i] != 0)
                    continue;
                dirNode.Di_addr[i] = (short)BitWorker.GetFirstFree(bitmap);
                blockNum = dirNode.Di_addr[i];
                break;
            }
            if (blockNum < 0)
                throw new OSException.BlockAppendException("Can't append block to dir");
            return blockNum;
        }

        public static bool WriteBlockToDir(System.IO.FileStream fs, short blockNum, int blockSize)
        {
            var bm = BitWorker.TurnBitOff(DataExtractor.GetBitmap(fs, 1920), blockNum);
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.BITMAP);
            FSPartsWriter.WriteBitmap(fs, bm, blockSize);
            return true;
        }

        public static bool WriteNodeToFile(System.IO.FileStream fs, INode dirNode, short nodeNum)
        {
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.INODES) + (nodeNum - 1) *
                OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.INODE);
            FSPartsWriter.WriteINode(fs, dirNode);
            return true;
        }
    }
}
