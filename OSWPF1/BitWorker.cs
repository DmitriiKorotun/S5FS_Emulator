using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class BitWorker
    {
        public static byte[] TurnBitOn(byte[] arr, int blockNum)
        {
            int byteIndex = (blockNum - 1) / 8;
            int bitIndex = blockNum % 8;
            if (blockNum % 8 == 0) //Need to remove it after refacktoring blocks structure. Start index should be 0 not 1
                bitIndex = 8;
            byte mask = (byte)(1 << bitIndex - 1); //To change: need to remove - 1
            arr[byteIndex] |= mask;
            return arr;
        }

        public static byte[] TurnBitOff(byte[] arr, int blockNum)
        {
            int byteIndex = (blockNum - 1) / 8;
            int bitIndex = blockNum % 8;
            if (blockNum % 8 == 0) //Need to remove it after refacktoring blocks structure. Start index should be 0 not 1
                bitIndex = 8;
            byte mask = (byte)(1 << bitIndex - 1);
            mask = (byte)~mask;
            arr[byteIndex] &= mask;
            return arr;
        }

        public static int ReadByte(byte myByte) //TO change name MyByte
        {
            int bitIndex = -1;
            var temp = myByte;
            for (int i = 0; i < 8; ++i)
            {
                myByte = temp;
                byte mask = (byte)(1 << i);
                myByte &= mask;
                if (myByte == 0)
                {
                    bitIndex = i + 1;
                    break;
                }
            }
            return bitIndex;
        }

        public static int GetFirstFree(byte[] byteArr)
        {
            int blockNum = -1;
            for (int i = 0; i < byteArr.Length; ++i)
            {
                if (byteArr[i] == 255)
                    continue;
                else
                {
                    blockNum = ReadByte(byteArr[i]) + 8 * i ;
                    break;
                }
            }
            return blockNum;
        }

        public static short[] GetFreeBits(byte[] byteArr, int bitsToFind)
        {
            List<short> addr_list = new List<short>();

            short[] addresses;
            for (int i = 0; i < byteArr.Length; ++i)
            {
                addr_list.AddRange(GetFreeBits(byteArr[i], i));

                if (addr_list.Count >= bitsToFind)
                    break;
            }

            if (addr_list.Count < bitsToFind)
                throw new Exception("Cannot find enough free bits");
            else
            {
                addresses = new short[bitsToFind];
                for (int i = 0; i < bitsToFind; ++i)
                    addresses[i] = addr_list[i];
            }

            return addresses;
        }

        public static List<short> GetFreeBits(byte myByte)
        {
            var addresses = new List<short>();

            for (int i = 0; i < 8; ++i)
            {
                byte mask = (byte)(1 << i);
                myByte &= mask; //Bit AND
                if (myByte == 0)
                {
                    addresses.Add((short)(i + 1));
                }
            }
            return addresses;
        }

        public static List<short> GetFreeBits(byte myByte, int byteNum)
        {
            var addresses = new List<short>();
            var temp = myByte;
            for (int i = 0; i < 8; ++i)
            {
                temp = myByte;
                byte mask = (byte)(1 << i);
                temp &= mask; //Bit AND
                if (temp == 0)
                {
                    addresses.Add((short)(i + 1 + byteNum * 8)); //byteNum * 8 - for global addresses, 
                    //byteNum - is a global number of byte in byte[]
                }
            }
            return addresses;
        }


    }
}
