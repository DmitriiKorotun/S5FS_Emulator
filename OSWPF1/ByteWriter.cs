using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class ByteWriter
    {
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

        //Need to revert to fs.WriteByte();
        //Fills the file with fixed num of junk('0') starting from the current position
        public static void WriteJunk(System.IO.FileStream fs, long numOfJunk) //Should return num of bytes that have been written
        {
            try
            {
                for (long i = 0; i < numOfJunk; ++i)
                    fs.WriteByte(0);
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
        }

        //Need to revert to fs.WriteByte();
        //Fills the file with fixed num of junk ('0') starting from the offset
        public static void WriteJunk(System.IO.FileStream fs, long numOfJunk, long offset) //Should return num of bytes that have been written
        {
            //Add condition for "fs.position < offset"
            fs.Position = offset;
            try
            {
                for (long i = 0; i < numOfJunk; ++i)
                    fs.WriteByte(0);
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
        }

        //Copies all the remaining data starting from the offset
        public static long CopyData(System.IO.FileStream fs1, System.IO.FileStream fs2, long offset, int blockSize) //Should return num of bytes that have been written
        {
            long bytesWritten = 0;
            try
            {
                if (offset > fs1.Length) //TO change
                    throw new IndexOutOfRangeException();

                fs1.Position = offset;
                fs2.Position = offset;

                while (fs1.Length - fs1.Position >= blockSize)
                {
                    var block = ByteReader.ReadBlock(fs1, blockSize);
                    fs2.Write(block, 0, block.Length);
                    bytesWritten += block.Length;
                }

                var lastBytes = ByteReader.ReadBytes(fs1, fs1.Length - fs1.Position);
                fs2.Write(lastBytes, 0, lastBytes.Length);
                bytesWritten += lastBytes.Length;
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }

            return bytesWritten;
        }

        //Copies fixed amount of bytes starting from offset
        public static long CopyBytes(System.IO.FileStream fs1, System.IO.FileStream fs2, long offset, long bytesToCopy, int blockSize)
        {
            long bytesWritten = 0;
            try
            {
                if (offset > fs1.Length) //TO change
                    throw new IndexOutOfRangeException();

                fs1.Position = offset;
                fs2.Position = offset;

                while (bytesToCopy - bytesWritten >= blockSize)
                {
                    var block = ByteReader.ReadBlock(fs1, blockSize);
                    fs2.Write(block, 0, block.Length);
                    bytesWritten += block.Length;
                }

                var lastBytes = ByteReader.ReadBytes(fs1, bytesToCopy - bytesWritten);
                fs2.Write(lastBytes, 0, lastBytes.Length);
                bytesWritten += lastBytes.Length;
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }


        //Copies fixed amount of bytes starting from fs1 current position
        public static long CopyBytes(System.IO.FileStream fs1, System.IO.FileStream fs2, long bytesToCopy, int blockSize)
        {
            long bytesWritten = 0;
            try
            {
                while (bytesToCopy - bytesWritten >= blockSize)
                {
                    var block = ByteReader.ReadBlock(fs1, blockSize);
                    fs2.Write(block, 0, block.Length);
                    bytesWritten += block.Length;
                }

                var lastBytes = ByteReader.ReadBytes(fs1, bytesToCopy - bytesWritten);
                fs2.Write(lastBytes, 0, lastBytes.Length);
                bytesWritten += lastBytes.Length;
            }
            catch (Exception e)
            {
                Logger.GetInstance("").Log(e.Message + Environment.NewLine);
                throw e;
            }
            return bytesWritten;
        }

        //Copies bytes from start pos to end pos to another file
        public static long CopyData(System.IO.FileStream fs1, System.IO.FileStream fs2, long startPos, long endPos, int blockSize)
        {
            long bytesWritten = 0, bytesToWrite = endPos - startPos;

            fs1.Position = startPos;
            fs2.Position = startPos;

            while (bytesToWrite - bytesWritten >= blockSize)
            {
                WriteBlock(fs2, blockSize, ByteReader.ReadBlock(fs1, blockSize));
                bytesWritten += blockSize;
            }

            var lastBytes = ByteReader.ReadBytes(fs1, bytesToWrite - bytesWritten);
            fs2.Write(lastBytes, 0, lastBytes.Length);
            bytesWritten += lastBytes.Length;

            return bytesWritten;
        }

        //Writes block to the fs starting from fs position
        public static long WriteBlock(System.IO.FileStream fs, int blockSize, byte[] arr)
        {
            if (arr.Length != blockSize)
                throw new ArgumentException("Block size and arr size doesn't match");

            fs.Write(arr, 0, arr.Length);
            return arr.Length;
        }

        //Writes block to the fs starting from offset
        public static long WriteBlock(System.IO.FileStream fs, long offset, int blockSize, byte[] arr)
        {
            if (arr.Length != blockSize)
                throw new ArgumentException("Block size and arr size doesn't match");

            if (offset > fs.Length)
                throw new IndexOutOfRangeException("Index of fs position is out of range");

            fs.Position = offset;
            fs.Write(arr, 0, arr.Length);

            return arr.Length;
        }
    }
}
