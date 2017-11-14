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
            int blockIndex = 0;
            if (addressBlocks > 13)
                blockIndex = addressBlocks + 1; //blockIndex - current not addr block index
            else
                blockIndex = 

            //Writes addresses into the following blocks
            for (int i = 0; i < addressBlocks; ++i)
            {
                var addrBlock = new byte[blockSize];
                // freeBlocks.Length - addressBlocks + i was addressBlocks + i
                for (int j = 0; j < addrBlock.Length && blockIndex < freeBlocks.Length; j += 2)
                {
                    var address = BitConverter.GetBytes(freeBlocks[blockIndex]);

                    addrBlock[j] = address[0];
                    addrBlock[j + 1] = address[1];

                    ++blockIndex;
                }
                dataToWrite.Add(freeBlocks[i], addrBlock);
            }

            //Writes data into the following blocks
            for (int i = addressBlocks; i < freeBlocks.Length; ++i)
            {
                var data = new byte[blockSize];
                for (int j = 0; j < data.Length; ++j)
                    data[j] = 1;
                dataToWrite.Add(freeBlocks[i], data);
            }
            return dataToWrite;
        }

        //It fills blocks with addresses and data
        private void SortBlocks(short[] freeBlocks, int blockSize)
        {
            const int addr_size = 13; //This is size of di_addr
            int blockCapacity = blockSize / 2;
            var addressBlocks = BlocksHandler.BlocksForAddress(freeBlocks.Length, blockSize);
            int depth = addressBlocks > 0 ? 1 : 0;

            if (addressBlocks > addr_size && addressBlocks < addr_size * blockCapacity)
                depth = 2;
            else
                throw new OutOfMemoryException();

            if (depth == 2)
            {
                int first_lvl_offset = blockCapacity * blockCapacity; // It actually store too much
                int second_lvl_offset = blockCapacity;
                for (int i = 0; i < addr_size; ++i)
                {

                    if (depth > 0)
                    {

                    }
                }
            }
            else if (depth == 1)
            {
                int first_lvl_offset = blockCapacity;
                for (int i = 0; i < addressBlocks; ++i)
                {
                    for (int j = 0; j < blockCapacity; ++j)
                    {
                        freeBlocks[i * blockCapacity] 
                    }
                    
                }
            }
            
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
