using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class ByteReader
    {
        //Read the whole block starting from fs current position
        public static byte[] ReadBlock(System.IO.FileStream fs, int blockSize)
        {
            var block = new byte[blockSize];
            fs.Read(block, 0, block.Length);
            return block;
        }

        //Read the whole block starting from offset position
        public static byte[] ReadBlock(System.IO.FileStream fs, int blockSize, long offset)
        {
            var block = new byte[blockSize];
            fs.Position = offset;
            fs.Read(block, 0, block.Length);
            return block;
        }

        //Reads the small amount of bytes starting from fs Position
        public static byte[] ReadBytes(System.IO.FileStream fs, long bytesToRead)
        {
            var byteArr = new byte[bytesToRead];
            fs.Read(byteArr, 0, byteArr.Length);
            return byteArr;
        }

        //Reads the big amount of bytes starting from fs Position
        public static byte[] ReadBytes(System.IO.FileStream fs, int blockSize, long bytesToRead)
        {
            if (bytesToRead < 1)
                throw new ArgumentException("Number of bytes to read is less than 1");

            var byteArr = new byte[bytesToRead];
            long bytesWritten = 0;
            while (bytesToRead - bytesWritten >= blockSize)
            {
                var block = ReadBlock(fs, blockSize);
                for (var i = 0; i < block.Length && bytesWritten < byteArr.Length; ++i)
                {
                    byteArr[bytesWritten] = block[i];
                    ++bytesWritten;
                }
            }

            var lastByteArr = ReadBytes(fs, bytesToRead - bytesWritten);
            if (lastByteArr.Length != 0)
            {
                for (var i = 0; i < lastByteArr.Length && bytesWritten < byteArr.Length; ++i)
                {
                    byteArr[bytesWritten] = lastByteArr[i];
                    ++bytesWritten;
                }
            }

            return byteArr;
        }
    }
}
