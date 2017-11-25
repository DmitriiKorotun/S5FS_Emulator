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
