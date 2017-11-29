using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class ByteWriter
    {
        public static void WriteJunk(byte[] block)
        {

        }

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

        private static byte[] CopyArr(byte[] dest, byte[] source, int startByteNum)
        {
            source.CopyTo(dest, startByteNum);
            return dest;
        }
    }
}
