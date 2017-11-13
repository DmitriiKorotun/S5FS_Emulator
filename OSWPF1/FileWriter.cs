using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FileWriter
    {
        //Returnes Dictionary with pair: block address - byte arr
        //I dont use real file data. Instead I just fill the blocks for file data with '1'
        public static Dictionary<int, byte[]> GetDataArr(short[] freeBlocks, int blockSize)
        {
            var dataToWrite = new Dictionary<int, byte[]>();
            var addressBlocks = BlocksHandler.BlocksForAddress(freeBlocks.Length, blockSize);
            for (int i = 0; i < addressBlocks; ++i)
            {
                var data = new byte[blockSize];
                var address = BitConverter.GetBytes(freeBlocks[addressBlocks + i]);
                for (int j = 0; j < data.Length; j += 2)
                {
                    data[j] = address[0];
                    data[j + 1] = address[1];
                }
                dataToWrite.Add(i, data);
            }
            for (int i = addressBlocks; i < freeBlocks.Length; ++i)
            {
                var data = new byte[blockSize];
                for (int j = 0; j < data.Length; ++j)
                    data[j] = 1;
                dataToWrite.Add(i, data);
            }
            return dataToWrite;
        }

        private void WriteBlock(System.IO.FileStream fs, byte[] data)
        {
            if (fs.CanWrite)
            {
                for (int i = 0; i < data.Length; ++i)
                    fs.WriteByte(data[i]);
            }
            else
                throw new System.IO.IOException();
        }

        //private void WriteBlocks(Dictionary<int, byte[]> data, Superblock superblock, string path, long offset)
        //{

        //    using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Append))
        //    {
        //        fs.Seek(offset, System.IO.SeekOrigin.Begin);
        //        long index = offset, ;
        //        while (index < 62914560)
        //        {
        //            fs.WriteByte()
        //        }
        //        for (int i = 0; i < 15260; ) //15260 - need to be changed for blocks num
        //        {

        //        }
        //        fs.Write()
        //    }
        //}
    }
}
