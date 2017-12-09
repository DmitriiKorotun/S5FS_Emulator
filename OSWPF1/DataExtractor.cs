using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSWPF1
{
    static class DataExtractor
    {
        static Superblock GetSuperblock(Superblock superblock, System.IO.FileStream fs)
        {
            superblock.ClusterSize = ByteConverter.ShortFromBytes(fs);
            superblock.FSType = ByteConverter.IntFromBytes(fs);
            superblock.INodeCount = ByteConverter.ShortFromBytes(fs);
            superblock.INodeSize = ByteConverter.ShortFromBytes(fs);
            superblock.FreeBlock = ByteConverter.ShortFromBytes(fs);
            superblock.FreeINode = ByteConverter.ShortFromBytes(fs);
            return superblock;
        }

        static Bitmap GetBitmap(Bitmap bitmap, System.IO.FileStream fs)
        {
            for (int i = 0; i < bitmap.BitmapValue.Length; ++i)
                bitmap.BitmapValue[i] = (byte)fs.ReadByte();
            return bitmap;
        }

        public static byte[] GetBitmap(System.IO.FileStream fs, int bmLength)
        {
            var bm = new byte[bmLength];
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.BITMAP);
            for (int i = 0; i < bm.Length; ++i)
                bm[i] = (byte)fs.ReadByte();
            return bm;
        }

        public static byte[] GetNodeMap(System.IO.FileStream fs, int bmLength)
        {
            var bm = new byte[bmLength];
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.IMAP);
            for (int i = 0; i < bm.Length; ++i)
                bm[i] = (byte)fs.ReadByte();
            return bm;
        }

        internal static int GetFreeBlockNum(Bitmap blockMap)
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
            Thread.Sleep(200);
            using (System.IO.FileStream fs = System.IO.File.Open("FS", System.IO.FileMode.Open,
System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
            {
                storage.Superblock = GetSuperblock(storage.Superblock, fs);
                fs.Seek(4096, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
                storage.Bitmap = GetBitmap(storage.Bitmap, fs);
                fs.Seek(4096 * 2, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
                storage.INodeMap = GetBitmap(storage.INodeMap, fs);
                fs.Seek(4096 * 3, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
            }
            return storage;
        }

        public static INode GetINode(System.IO.FileStream fs, int nodeNum)
        {
            var node = new INode();
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.INODES) +
                OffsetHandbook.GetOffs(OffsetHandbook.sizeGuide.INODE) * (nodeNum - 1);
            var nodeInBytes = new byte[INode.Offset];
            for (int i = 0; i < nodeInBytes.Length; ++i)
                nodeInBytes[i] = (byte)fs.ReadByte();

            node.Flag.System = Convert.ToBoolean(nodeInBytes[0]);
            node.Flag.Hidden = Convert.ToBoolean(nodeInBytes[1]);
            node.Flag.Type = Convert.ToBoolean(nodeInBytes[2]);

            var size = new byte[4] { nodeInBytes[4], nodeInBytes[5], nodeInBytes[6], nodeInBytes[7] };
            node.Size = BitConverter.ToInt32(size, 0);

            var uid = new byte[2] { nodeInBytes[8], nodeInBytes[9] };
            var gid = new byte[2] { nodeInBytes[10], nodeInBytes[11] };
            node.UID = BitConverter.ToInt16(uid, 0);
            node.GID = BitConverter.ToInt16(gid, 0);

            var creationDate = new byte[8] { nodeInBytes[12], nodeInBytes[13], nodeInBytes[14], nodeInBytes[15],
            nodeInBytes[16], nodeInBytes[17], nodeInBytes[18], nodeInBytes[19]};
            var changeDate = new byte[8] { nodeInBytes[20], nodeInBytes[21], nodeInBytes[22], nodeInBytes[23],
            nodeInBytes[24], nodeInBytes[25], nodeInBytes[26], nodeInBytes[27]};
            node.CreationDate = BitConverter.ToInt64(creationDate, 0);
            node.ChangeDate = BitConverter.ToInt64(changeDate, 0);

            int bytePos = 28;
            for (int i = 0; i < node.Di_addr.Length; ++i)
            {
                var addrBlock = new byte[2];
                addrBlock[0] = nodeInBytes[bytePos];
                addrBlock[1] = nodeInBytes[bytePos + 1];
                bytePos += 2;
                node.Di_addr[i] = BitConverter.ToInt16(addrBlock, 0);
            }
            node.Rights = BitConverter.ToInt16(nodeInBytes, 54);

            return node;
        }
    }
}
