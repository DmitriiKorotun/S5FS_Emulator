﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class BlocksHandler
    {
        //Number of blocks used to store only file data (without blocks addresses)
        public static int BlocksForFile(int blockSize, int filesize)
        {
            return (int)Math.Ceiling((double)filesize / blockSize);
        }

        //Returnes short arr with blocks addresses
        private short[] GetBlocksArr(Bitmap bitmap, int blockNum)
        {
            var blockList = new List<short>();
            short index = 0;

            while (blockList.Count < blockNum && index < bitmap.BitmapValue.Length)
            {
                if (bitmap.BitmapValue[index] == 0)
                    blockList.Add(index);
                ++index;
            }

            if (index == bitmap.BitmapValue.Length && blockList.Count < blockNum) //Checks if FS has enough size to write the file
                throw new OutOfMemoryException();

            short[] blocksArr = new short[blockNum];
            for (int i = 0; i < blockNum; ++i)
                blocksArr[i] = blockList[i];

            return blocksArr;
        }

        // Returnes num of blocks needed to write the file
        private int GetBlocksNum(int blocksNum, int blockSize)
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
        private int BlocksForAddress(int blocksNum, int blockSize)
        {
            int blocksForAddress = 0;
            if (blocksNum > 13)
            {
                blocksForAddress = (int)Math.Ceiling((double)blocksNum / (blockSize / 2));
            }
            return blocksForAddress;
        }
    }
}
