using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class BlocksHandler
    {
        //Number of blocks used to store only file data (without blocks addresses)
        static int BlocksForFile(int blockSize, int filesize)
        {
            return (int)Math.Ceiling((double)filesize / blockSize);
        }

        //Returnes short arr with blocks addresses
        //Starts with 1
        public static short[] GetBlocksArr(Bitmap bitmap, int filesize, int blockSize)
        {
            var blockNum = GetBlocksNum(BlocksForFile(blockSize, filesize), blockSize);
            if (bitmap.BitmapValue.Length * 8 < blockNum)
                throw new OutOfMemoryException();

            return BitWorker.GetFreeBits(bitmap.BitmapValue, blockNum);
        }

        // Returnes num of blocks needed to write the file
        static int GetBlocksNum(int blocksNum, int blockSize)
        {
            int blocksNeeded = 0;
            if (blocksNum <= 13) // 13 is length of di_addr[] array
            {
                blocksNeeded = blocksNum;
            }
            else
            {
                int blockCapacity = blockSize / 2; //How many adresses of short type can be stored in one block
                blocksNeeded = blocksNum + (int)Math.Ceiling((double)blocksNum / blockCapacity); //Need to be edited cause can use one unnecessary block
                //blocksNeeded = blocks used to store fileData + blocks used to store blocks adresses
            }
            return blocksNeeded;
        }

        // Returnes num of blocks needed to write the addresses
        public static int BlocksForAddress(int blocksNum, int blockSize)
        {
            int blocksForAddress = 0;
            if (blocksNum > 13)
            {
                blocksForAddress = (int)Math.Ceiling((double)blocksNum / (blockSize / 2));
            }
            return blocksForAddress;
        }

        //Returnes Dictionary with pair: block address - byte arr
        //I dont use real file data. Instead I just fill the blocks for file data with '1'
        public static Dictionary<int, byte[]> GetDataArr(short[] freeBlocks, byte[] data, int blockSize)
        {
            var dataToWrite = new Dictionary<int, byte[]>();

            const int di_addr_size = 13, startDataBlocks = di_addr_size - 1;

            var addressBlocks = BlocksHandler.BlocksForAddress(freeBlocks.Length, blockSize);
            int blockIndex = startDataBlocks + addressBlocks;

            for (int i = 0; i < startDataBlocks && i < freeBlocks.Length; ++i)
                dataToWrite.Add(freeBlocks[i], WriteBlock(new byte[blockSize], data, i * blockSize));

            //Writes addresses into the following blocks
            for (int i = startDataBlocks; i < startDataBlocks + addressBlocks; ++i)
            {
                dataToWrite.Add(freeBlocks[i], WriteAddrBlock(new byte[blockSize], freeBlocks, blockIndex));
                blockIndex += blockSize / 2;
            }

            //Writes data into the following blocks
            for (int i = startDataBlocks + addressBlocks; i < freeBlocks.Length; ++i)
            {
                dataToWrite.Add(freeBlocks[i], WriteBlock(new byte[blockSize], data, i * blockSize));
            }
            return dataToWrite;
        }

        private static byte[] WriteAddrBlock(byte[] block, short[] freeBlocks, int blockIndex)
        {
            //This is an algorithm for 1 level addressation          
            for (int i = 0; i < block.Length && blockIndex < freeBlocks.Length; i += 2)
            {
                var address = BitConverter.GetBytes(freeBlocks[blockIndex]);
                block[i] = address[0];
                block[i + 1] = address[1];
                ++blockIndex;
            }
            return block;
        }

        private static byte[] WriteBlock(byte[] block, byte[] data, int startIndex)
        {
            for (int i = 0; i < block.Length && startIndex + i < data.Length; ++i)
                block[i] = data[startIndex + i];
            return block;
        }

        public static byte[] GetBlock(System.IO.FileStream fs, int blockSize, short blockNum)
        {
            var block = new byte[blockSize];
            fs.Position = OffsetHandbook.GetPos(OffsetHandbook.posGuide.MAINDIR) + (blockNum - 1) * blockSize;
            fs.Read(block, 0, block.Length);
            return block;
        }

        public static bool IsBlocksMany(int size, int blockSize)
        {
            var isBlocksMany = size / blockSize > 13 ? true : false;
            return isBlocksMany;
        }
    }
}
