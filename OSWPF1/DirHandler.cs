using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DirHandler
    {
        public static byte[] WriteFileDirInfo(byte[] block, byte[] data, int startByteNum)
        {
            data.CopyTo(block, startByteNum);
            return block;
        }

        public static void DeleteDir()
        {

        }

        private static void WriteDir(System.IO.FileStream fs, INode iNode, FileDataStorage storage, int nodeNum)
        {
            using (fs)
            {
                fs.Position = OffsetHandbook.GetBitmapStart();
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

            var dataDict = BlocksHandler.GetDataArr(blocks, storage.Superblock.ClusterSize);
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
    }
}
