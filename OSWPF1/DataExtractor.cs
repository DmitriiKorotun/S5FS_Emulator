using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    static class DataExtractor
    {
        static void GetSuperblock(Superblock superblock, System.IO.FileStream fs)
        {
            superblock.ClusterSize = ByteConverter.ShortFromBytes(fs, 2);
            superblock.FSType = ByteConverter.IntFromBytes(fs, 4);
            superblock.INodeCount = ByteConverter.ShortFromBytes(fs, 2);
            superblock.INodeSize = ByteConverter.ShortFromBytes(fs, 2);
            superblock.FreeBlock = ByteConverter.ShortFromBytes(fs, 2);
            superblock.FreeINode = ByteConverter.ShortFromBytes(fs, 2); 
        }

        static void GetBitmap(Bitmap bitmap, System.IO.FileStream fs)
        {
            for (int i = 0; i < bitmap.BitmapValue.Length; ++i)
                bitmap.BitmapValue[i] = (byte)fs.ReadByte();
        }

        // It returnes INode map
        //static bool GetINodeMap(INodeMap iNodeMap, System.IO.FileStream fs, Superblock superblock)
        //{
        //    int count = 0, i = 0;
        //    while (iNodeMap.NodeAdress[iNodeMap.NodeAdress.Length - 1] != 0 &&
        //        count < superblock.INodeCount)
        //    {
        //        byte iNode = (byte)fs.ReadByte();
        //        if (iNode == 0)
        //        {
        //            iNodeMap.NodeAdress[i] = (short)count;
        //            ++i;
        //        }
        //        ++count;
        //    }
        //    bool isNodeLeft = count < superblock.INodeCount ? true : false;
        //    return isNodeLeft;
        //}

        internal static int GetFreeBlock(Bitmap blockMap)
        {
            int i = 0, blockNum = -1;
            bool isFound = false;
            while (i < blockMap.BitmapValue.Length && !isFound)
            {
                if (blockMap.BitmapValue[i] == 0)
                {
                    blockNum = i + 1;
                    isFound = true;
                }
                ++i;
            }
            return blockNum;
        }

        // It returns num of first free iNode
        internal static int GetINodeNum(Bitmap nodeMap)
        {
            int adress = -1;
            adress = BitWorker.GetFirstFree(nodeMap.BitmapValue);
            return adress;
        }

        // It searches the iNode map and if map contain free iNode it will return num of this iNode
        // If map doesn't contain free iNode, GetINode() will try to get another map of iNodes
        //internal static int GetINode(FileDataStorage storage, System.IO.FileStream fs)
        //{
        //    int num = GetINodeNum(storage.INodeMap);
        //    if (num < 0)
        //    {
        //        GetINodeMap(storage.INodeMap, fs, storage.Superblock);
        //        num = GetINodeNum(storage.INodeMap);
        //    }
        //    return num;
        //}

        // It returnes main information about File System
        public static FileDataStorage GetData(string filepath)
        {
            var storage = new FileDataStorage();
            using (var fs = System.IO.File.OpenRead(filepath))
            {
                GetSuperblock(storage.Superblock, fs);
                fs.Seek(4096, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
                GetBitmap(storage.Bitmap, fs);
                fs.Seek(4096 * 2, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
                GetBitmap(storage.INodeMap, fs);
                fs.Seek(4096 * 3, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
            }
            return storage;
        }
    }
}
