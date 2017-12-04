using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FSPartsWriter
    {
        public static long WriteBitmap(System.IO.FileStream fs, Bitmap bitmap, int blockSize)
        {
            long bytesWritten = 0;
            foreach (byte byteElem in bitmap.BitmapValue)
            {
                fs.WriteByte(byteElem);
                ++bytesWritten;
            }
            while (fs.Position % blockSize != 0)
            {
                fs.WriteByte(0);
                ++bytesWritten;
            }
            return bytesWritten;
        }

        public static long WriteBitmap(System.IO.FileStream fs, byte[] bitmap, int blockSize)
        {
            long bytesWritten = 0;
            foreach (byte byteElem in bitmap)
            {
                fs.WriteByte(byteElem);
                ++bytesWritten;
            }
            while (fs.Position % blockSize != 0)
            {
                fs.WriteByte(0);
                ++bytesWritten;
            }
            return bytesWritten;
        }

        public static void WriteSuperblock(System.IO.FileStream fs, Superblock superblock)
        {
            fs.Write(BitConverter.GetBytes(superblock.ClusterSize), 0, BitConverter.GetBytes(superblock.ClusterSize).Length);
            fs.Write(BitConverter.GetBytes(superblock.FSType), 0, BitConverter.GetBytes(superblock.FSType).Length);
            fs.Write(BitConverter.GetBytes(superblock.INodeCount), 0, BitConverter.GetBytes(superblock.INodeCount).Length);
            fs.Write(BitConverter.GetBytes(superblock.INodeSize), 0, BitConverter.GetBytes(superblock.INodeSize).Length);
            fs.Write(BitConverter.GetBytes(superblock.FreeBlock), 0, BitConverter.GetBytes(superblock.FreeBlock).Length);
            fs.Write(BitConverter.GetBytes(superblock.FreeINode), 0, BitConverter.GetBytes(superblock.FreeINode).Length);
            while (fs.Position % superblock.ClusterSize != 0)
                fs.WriteByte(0);
        }

        public static long WriteINodes(System.IO.FileStream fs, INode[] iNodes)
        {
            long bytesWritten = 0;
            for (int i = 0; i < iNodes.Length; ++i)
            {
                fs.Write(BitConverter.GetBytes(iNodes[i].Flag.System), 0, BitConverter.GetBytes(iNodes[i].Flag.System).Length);
                fs.Write(BitConverter.GetBytes(iNodes[i].Flag.Hidden), 0, BitConverter.GetBytes(iNodes[i].Flag.Hidden).Length);
                fs.Write(BitConverter.GetBytes(iNodes[i].Flag.Type), 0, BitConverter.GetBytes(iNodes[i].Flag.Type).Length);
                fs.WriteByte(0);
                fs.Write(BitConverter.GetBytes(iNodes[i].Size), 0, BitConverter.GetBytes(iNodes[i].Size).Length);
                fs.Write(BitConverter.GetBytes(iNodes[i].UID), 0, BitConverter.GetBytes(iNodes[i].UID).Length);
                fs.Write(BitConverter.GetBytes(iNodes[i].GID), 0, BitConverter.GetBytes(iNodes[i].GID).Length);
                fs.Write(BitConverter.GetBytes(iNodes[i].CreationDate), 0, BitConverter.GetBytes(iNodes[i].CreationDate).Length);
                fs.Write(BitConverter.GetBytes(iNodes[i].ChangeDate), 0, BitConverter.GetBytes(iNodes[i].ChangeDate).Length);

                for (int j = 0; j < iNodes[i].Di_addr.Length; ++j)
                    fs.Write(BitConverter.GetBytes(iNodes[i].Di_addr[j]), 0, BitConverter.GetBytes(iNodes[i].Di_addr[j]).Length);
                bytesWritten += INode.Offset; //54 is the size of one INode
                fs.Write(BitConverter.GetBytes(iNodes[i].Rights), 0, BitConverter.GetBytes(iNodes[i].Rights).Length);
            }
            return bytesWritten;
        }

        public static long WriteINode(System.IO.FileStream fs, INode iNode)
        {
            long bytesWritten = 0;

            fs.Write(BitConverter.GetBytes(iNode.Flag.System), 0, BitConverter.GetBytes(iNode.Flag.System).Length);
            fs.Write(BitConverter.GetBytes(iNode.Flag.Hidden), 0, BitConverter.GetBytes(iNode.Flag.Hidden).Length);
            fs.Write(BitConverter.GetBytes(iNode.Flag.Type), 0, BitConverter.GetBytes(iNode.Flag.Type).Length);
            fs.WriteByte(0);
            fs.Write(BitConverter.GetBytes(iNode.Size), 0, BitConverter.GetBytes(iNode.Size).Length);
            fs.Write(BitConverter.GetBytes(iNode.UID), 0, BitConverter.GetBytes(iNode.UID).Length);
            fs.Write(BitConverter.GetBytes(iNode.GID), 0, BitConverter.GetBytes(iNode.GID).Length);
            fs.Write(BitConverter.GetBytes(iNode.CreationDate), 0, BitConverter.GetBytes(iNode.CreationDate).Length);
            fs.Write(BitConverter.GetBytes(iNode.ChangeDate), 0, BitConverter.GetBytes(iNode.ChangeDate).Length);

            for (int j = 0; j < iNode.Di_addr.Length; ++j)
                fs.Write(BitConverter.GetBytes(iNode.Di_addr[j]), 0, BitConverter.GetBytes(iNode.Di_addr[j]).Length);
            bytesWritten += INode.Offset;

            fs.Write(BitConverter.GetBytes(iNode.Rights), 0, BitConverter.GetBytes(iNode.Rights).Length);

            return bytesWritten;
        }
    }
}
