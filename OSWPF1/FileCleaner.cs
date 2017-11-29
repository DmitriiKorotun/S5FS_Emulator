using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FileCleaner
    {
        public static void DelFile(FileDataStorage storage, int nodeNum)
        {
            INode node;
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                node = DataExtractor.GetINode(fs, nodeNum);
                SetBlocksFree(fs, node, storage.Bitmap.BitmapValue, storage.Superblock.ClusterSize);
            }
        }

        public static void DelDir(FileDataStorage storage, int nodeNum)
        {
            INode node;
            using (System.IO.FileStream fs = System.IO.File.OpenRead("FS"))
            {
                node = DataExtractor.GetINode(fs, nodeNum);
                for (int i = 0; i < node.Di_addr.Length; ++i)
                {
                    if (node.Di_addr[i] != 0)
                    {
                        var block = BlocksHandler.GetBlock(fs, storage.Superblock.ClusterSize, node.Di_addr[i]);
                        for (int j = 16; j < block.Length; 
                            j += OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.FILEINDIR))
                        {
                            var fileNodeNum = ByteConverter.ShortFromBytes(fs);
                            var isDir = Convert.ToBoolean(fs.ReadByte());
                            if (isDir)
                                DelDir(storage, fileNodeNum);
                            else
                                DelFile(storage, nodeNum);
                        }
                    }
                }
            }
        }

        private static void SetBlocksFree(System.IO.FileStream fs, INode node, byte[] bitmap, int blockSize)
        {
            for (int i = 0; i < node.Di_addr.Length - 1; ++i)
            {
                if (node.Di_addr[i] != 0)
                    BitWorker.TurnBitOff(bitmap, node.Di_addr[i]);
            }
            if (BlocksHandler.IsBlocksMany(node.Size, blockSize))
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

        private static void SetNodeFree(System.IO.FileStream fs, byte[] nodeMap, int nodeNum)
        {
            BitWorker.TurnBitOff(nodeMap, nodeNum);
        }


    }
}
