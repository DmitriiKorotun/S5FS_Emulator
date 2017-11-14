using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class DataWriter
    {
        public static long WriteSuperblock(System.IO.FileStream fs, Superblock superblock)
        {
            fs.Write(BitConverter.GetBytes(superblock.ClusterSize), 0, BitConverter.GetBytes(superblock.ClusterSize).Length);
            fs.Write(BitConverter.GetBytes(superblock.FSType), 0, BitConverter.GetBytes(superblock.FSType).Length);
            fs.Write(BitConverter.GetBytes(superblock.INodeCount), 0, BitConverter.GetBytes(superblock.INodeCount).Length);
            fs.Write(BitConverter.GetBytes(superblock.INodeSize), 0, BitConverter.GetBytes(superblock.INodeSize).Length);
            fs.Write(BitConverter.GetBytes(superblock.FreeBlock), 0, BitConverter.GetBytes(superblock.FreeBlock).Length);
            fs.Write(BitConverter.GetBytes(superblock.FreeINode), 0, BitConverter.GetBytes(superblock.FreeINode).Length);
            return fs.Position; //Returnes num of bytes that have been written
        }

        public static long WriteBitmap(System.IO.FileStream fs, Bitmap bitmap)
        {
            long bytesWritten = 0;
            foreach (byte byteElem in bitmap.BitmapValue)
            {
                fs.WriteByte(byteElem);
                ++bytesWritten;
            }
            return bytesWritten;
        }

        //public static long WriteINodeMap(System.IO.FileStream fs, INodeMap nodeMap)
        //{
        //    long bytesWritten = 0;
        //    foreach (short iNode in nodeMap.NodeAdress)
        //    {
        //        fs.Write(BitConverter.GetBytes(iNode), 0, BitConverter.GetBytes(iNode).Length);
        //        bytesWritten += 2;
        //    }
        //    return bytesWritten;
        //}

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
                bytesWritten += 54; //54 is the size of one INode
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
            bytesWritten += 54; //54 is the size of one INode

            return bytesWritten;
        }

        public static long WriteJunk(System.IO.FileStream fs, long numOfJunk)
        {
            long bytesWritten = 0;
            try
            {
                if (numOfJunk > 62914560)
                    throw new OutOfMemoryException();
                for (long i = 0; i < numOfJunk; ++i)
                {
                    fs.WriteByte(0);
                    ++bytesWritten;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }

        //Fills the file with fixed num of junk ('0') startin from the offset
        public static long WriteJunk(System.IO.FileStream fs, long numOfJunk, long offset)
        {
            long bytesWritten = 0;
            try
            {
                if (numOfJunk > 62914560 - offset)
                    throw new OutOfMemoryException();

                fs.Seek(offset, System.IO.SeekOrigin.Begin);

                for (long i = 0; i < numOfJunk; ++i)
                {
                    fs.WriteByte(0);
                    ++bytesWritten;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }

        //Copies all the renaining data starting from the offset
        public static long CopyData(System.IO.FileStream fs1, System.IO.FileStream fs2, long offset)
        {
            long bytesWritten = 0, poisition = offset;
            try
            {
                if (offset > fs1.Length) //TO change
                    throw new IndexOutOfRangeException();

                fs1.Seek(offset, System.IO.SeekOrigin.Begin);
                fs2.Seek(offset, System.IO.SeekOrigin.Begin);

                while (poisition < fs1.Length)
                {
                    fs2.WriteByte((byte)fs1.ReadByte());
                    ++poisition;
                    ++bytesWritten;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }

        //Copies fixed amount of bytes starting from offset
        public static long CopyBytes(System.IO.FileStream fs1, System.IO.FileStream fs2, long offset, long bytesToCopy)
        {
            long bytesWritten = 0, poisition = offset;
            try
            {
                if (offset > fs1.Length) //TO change
                    throw new IndexOutOfRangeException();

                fs1.Position = offset;
                fs2.Position = offset;
                // fs1.Seek(offset, System.IO.SeekOrigin.Begin);
                // fs2.Seek(offset, System.IO.SeekOrigin.Begin);

                while (poisition < fs1.Length && bytesWritten < bytesToCopy)
                {
                    fs2.WriteByte((byte)fs1.ReadByte());
                    ++poisition;
                    ++bytesWritten;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }

        //Copies fixed amount of bytes starting from offset
        public static long CopyBytes(System.IO.FileStream fs1, System.IO.FileStream fs2, long bytesToCopy)
        {
            long bytesWritten = 0, position = fs1.Position;
            try
            {
                while (position < fs1.Length && bytesWritten < bytesToCopy)
                {
                    fs2.WriteByte((byte)fs1.ReadByte());
                    ++position;
                    ++bytesWritten;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }

        //Copies bytes from start pos to end pos to another file
        public static long CopyData(System.IO.FileStream fs1, System.IO.FileStream fs2, long startPos, long endPos)
        {
            long bytesWritten = 0, poisition = startPos;
            try
            {
                if (startPos > fs1.Length) //TO change
                    throw new IndexOutOfRangeException();

                fs1.Position = startPos;
                fs2.Position = startPos;

                while (poisition < fs1.Length && poisition < endPos)
                {
                    fs2.WriteByte((byte)fs1.ReadByte());
                    ++poisition;
                    ++bytesWritten;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }

        public static long WriteBlock(System.IO.FileStream fs, int blockSize, byte[] arr)
        {
            if (arr.Length != blockSize)
                throw new ArgumentException();

            fs.Write(arr, 0, arr.Length);
            //foreach (byte elem in arr)
            //    fs.WriteByte(elem);

            return arr.Length;
        }

        public static long WriteBlock(System.IO.FileStream fs, long offset, int blockSize, byte[] arr)
        {
            if (arr.Length != blockSize)
                throw new ArgumentException();

            if (offset > fs.Length)
                throw new IndexOutOfRangeException();

            fs.Seek(offset, System.IO.SeekOrigin.Begin);

            foreach (byte elem in arr)
                fs.WriteByte(elem);

            return arr.Length;
        }
    }
}
