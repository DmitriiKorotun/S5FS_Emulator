using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DirHandler
    {
        //public static byte[] WriteFileDirInfo(byte[] block, byte[] data, int startByteNum)
        //{
        //    data.CopyTo(block, startByteNum);
        //    return block;
        //}
        private static byte[] ComposeInfo(string name, short nodeNum, bool type)
        {
            var data = new byte[OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR)];
            byte[] bName = Encoding.ASCII.GetBytes(name);
            if (bName.Length > 16)
                throw new Exception("FileName is too long");
            bName.CopyTo(data, 0);
            BitConverter.GetBytes(nodeNum).CopyTo(data, 16);
          //  Convert.ToByte(type);
            BitConverter.GetBytes(type).CopyTo(data, 18);
            return data;
        }

        private static byte[] GetFreeBlock(System.IO.FileStream fs, short[] di_addr, int blockSize)
        {
            var block = new byte[blockSize];
            for (int i = 0; i < di_addr.Length; ++i)
            {
                if (di_addr[i] == 0)
                    continue;
                block = BlocksHandler.GetBlock(fs, blockSize, di_addr[i]);
                if (GetOffset(block) > -1)
                    break;
                else if (i == di_addr.Length - 1)
                    throw new InsufficientMemoryException("Dir hasn't free blocks");
            }
            return block;
        }

        private static int GetOffset(byte[] block)
        {
            var offset = -1;
            for (int i = 0; i < block.Length / OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR);
                i += OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR))
            {
                bool isFree = true;
                for (int j = 0; j < OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR); ++j)
                {
                    if (block[j + i * OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR)] == 0)
                    {
                        isFree = false;
                        break;
                    }
                }
                offset = isFree ? i : -1;
            }
            return offset;
        }


        private static byte[] WriteDirInfo(byte[] block, byte[] data, int startIndex)
        {
            if (startIndex < 0)
                throw new Exception("Current block hasn't free space");
            if (data.Length != OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR))
                throw new Exception("Data size isn't correct");
            data.CopyTo(block, startIndex);
            return block;
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

        private static void PrepDirData(INode iNode, FileDataStorage storage, string path)
        {
            int nodeNum = DataExtractor.GetINodeNum(storage.INodeMap);
            var blocks = BlocksHandler.GetBlocksArr(storage.Bitmap, iNode.Size, storage.Superblock.ClusterSize);
            for (int i = 0; i < blocks.Length; ++i)
            {
                storage.Bitmap.BitmapValue = BitWorker.TurnBitOn(storage.Bitmap.BitmapValue, blocks[i]);
            }
            storage.INodeMap.BitmapValue = BitWorker.TurnBitOn(storage.INodeMap.BitmapValue, nodeNum);

            var dataDict = BlocksHandler.GetDataArr(blocks, new byte[] { 0 } , storage.Superblock.ClusterSize);
            List<int> dataKeys = new List<int>(dataDict.Keys);
            for (int index = 0; index < iNode.Di_addr.Length && index < dataDict.Count; ++index)
            {
                iNode.Di_addr[index] = (short)dataKeys[index]; //To change short for int cause data can be lost
            }

            WriteDir(System.IO.File.OpenWrite(path), iNode, storage, nodeNum);
        }

        public static void WriteDir(INode iNode, string path)
        {
            PrepDirData(iNode, DataExtractor.GetData(path), path);
        }

        public static void AddFileToDir(string name, int blockSize, short dirNode, short fileNode, bool type)
        {
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open))
            {
                var block = GetFreeBlock(fs, DataExtractor.GetINode(fs, dirNode).Di_addr, blockSize);
                // AppendBlockToDir(DataExtractor.GetINode(fs, dirNode), )
                ByteWriter.WriteBlock(fs, blockSize,
                     WriteDirInfo(block, ComposeInfo(name, fileNode, type), GetOffset(block)));
            }
        }

        public static INode AppendBlockToDir(INode dirNode, byte[] bitmap)
        {
            for (int i = 0; i < dirNode.Di_addr.Length; ++i)
            {
                if (dirNode.Di_addr[i] != 0)
                    continue;
                dirNode.Di_addr[i] = (short)BitWorker.GetFirstFree(bitmap);
                BitWorker.TurnBitOff(bitmap, dirNode.Di_addr[i]);
                break;
            }
            return dirNode;
        }
    }
}
