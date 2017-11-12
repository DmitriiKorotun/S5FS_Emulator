using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FSHandler
    {
        public FSHandler(string filepath)
        {
            SetPath(filepath);
        }

        // Path to the FS file
        string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        void SetPath(string path)
        {
            if (System.IO.File.Exists(path))
            {
                using (Logger.GetInstance(path + ".log"))
                {
                    Logger.GetInstance(path + ".log").Log(DateTime.Now +
                        " - Logpath has been set to " + path + ".log" + Environment.NewLine);
                    Logger.GetInstance(null).Log(DateTime.Now +
                        " - FSHandle has set path to file. Path is " + path + Environment.NewLine);
                }
            }
            else
            {
                CreateMainFile(path);
            }
            Path = path;
        }

        //It creates the FS file and wrytes the superblock info to this file
        //After that it will fill the rest of the file with '0'
        void CreateMainFile(string filepath)
        {
            using (Logger.GetInstance(path + ".log"))
            {
                System.IO.FileStream fs = null;
                try
                {
                    fs = System.IO.File.Create(path);
                    Logger.GetInstance(null).Log(DateTime.Now + " - MainFile was created with name: " + path
                        + Environment.NewLine);

                    var iniHandler = new IniFiles.IniHandler(path + "_data.ini");
                    Logger.GetInstance(null).Log(DateTime.Now + " - IniHandler was initialized with" +
                        " filename: " + path + "_data.ini" + Environment.NewLine);

                    var superblock = new Superblock();

                    superblock.ClusterSize = Convert.ToInt16(iniHandler.ReadINI("Superblock", "BlockSize"));
                    byte[] ftype = Encoding.ASCII.GetBytes(iniHandler.ReadINI("Superblock", "Name"));
                    superblock.FSType = BitConverter.ToInt32(ftype, 0);
                    superblock.INodeCount = Convert.ToInt16(iniHandler.ReadINI("Superblock", "INodeCount"));
                    superblock.INodeSize = Convert.ToInt16(iniHandler.ReadINI("Superblock", "INodeSize"));
                    superblock.FreeBlock = Convert.ToInt16(iniHandler.ReadINI("Superblock", "FreeBlocksCount"));
                    superblock.FreeINode = Convert.ToInt16(iniHandler.ReadINI("Superblock", "FreeINodeCount"));

                    fs.Write(BitConverter.GetBytes(superblock.ClusterSize), 0, BitConverter.GetBytes(superblock.ClusterSize).Length);
                    fs.Write(BitConverter.GetBytes(superblock.FSType), 0, BitConverter.GetBytes(superblock.FSType).Length);
                    fs.Write(BitConverter.GetBytes(superblock.INodeCount), 0, BitConverter.GetBytes(superblock.INodeCount).Length);
                    fs.Write(BitConverter.GetBytes(superblock.INodeSize), 0, BitConverter.GetBytes(superblock.INodeSize).Length);
                    fs.Write(BitConverter.GetBytes(superblock.FreeBlock), 0, BitConverter.GetBytes(superblock.FreeBlock).Length);
                    fs.Write(BitConverter.GetBytes(superblock.FreeINode), 0, BitConverter.GetBytes(superblock.FreeINode).Length);

                    for (int i = 0; i < 62914560 - 16; ++i) //16 is Superblock size
                        fs.WriteByte(0);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(null).Log(DateTime.Now + " - There was an EXCEPTION: "
                        + e.Message + Environment.NewLine + "EXCEPTION with path: " + path);
                    throw e;
                }
                finally
                {
                    if (fs != null)
                        fs.Dispose();
                }
            }
        }


        //public void CreateUserGroup(long offset, System.IO.FileStream fs)
        //{
        //    fs.Seek(offset, System.IO.SeekOrigin.Begin);
        //    fs.Write()
        //}

        private bool WriteFile(FileDataStorage storage, INode iNode)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(Path),
                temp = new System.IO.FileStream(fs.Name + ".temp", System.IO.FileMode.Create))
            {
                int freeBlock = DataExtractor.GetFreeBlock(storage.Bitmap);
                int nodeNum = DataExtractor.GetINode(storage, fs);
                int i = 0;

                while (i != (storage.Superblock.UsedBlock * storage.Superblock.ClusterSize + freeBlock - 1))
                {
                    temp.WriteByte((byte)fs.ReadByte());
                    ++i;
                }

                temp.WriteByte(1);
                fs.ReadByte();
                ++i;

                while (i != ((storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock) *
                    storage.Superblock.ClusterSize + nodeNum - 1))
                {
                    temp.WriteByte((byte)fs.ReadByte());
                    ++i;
                }

                temp.WriteByte(1);
                fs.ReadByte();
                ++i;

                while (i != (storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock +
                    storage.INodeMap.UsedBlock) * storage.Superblock.ClusterSize + nodeNum - 1)
                {
                    temp.WriteByte((byte)fs.ReadByte());
                    ++i;
                }

                temp.Write(BitConverter.GetBytes(iNode.Flag.System), 0, BitConverter.GetBytes(iNode.Flag.System).Length);
                temp.Write(BitConverter.GetBytes(iNode.Flag.Hidden), 0, BitConverter.GetBytes(iNode.Flag.Hidden).Length);
                temp.Write(BitConverter.GetBytes(iNode.Flag.Type), 0, BitConverter.GetBytes(iNode.Flag.Type).Length);
                temp.WriteByte(0);
                temp.Write(BitConverter.GetBytes(iNode.Size), 0, BitConverter.GetBytes(iNode.Size).Length);
                temp.Write(BitConverter.GetBytes(iNode.UID), 0, BitConverter.GetBytes(iNode.UID).Length);
                temp.Write(BitConverter.GetBytes(iNode.GID), 0, BitConverter.GetBytes(iNode.GID).Length);
                temp.Write(BitConverter.GetBytes(iNode.CreationDate), 0, BitConverter.GetBytes(iNode.CreationDate).Length);
                temp.Write(BitConverter.GetBytes(iNode.ChangeDate), 0, BitConverter.GetBytes(iNode.ChangeDate).Length);

                for (int j = 0; j < iNode.Di_addr.Length; ++j)
                    temp.Write(BitConverter.GetBytes(iNode.Di_addr[j]), 0, BitConverter.GetBytes(iNode.Di_addr[j]).Length);

                fs.Seek(54, System.IO.SeekOrigin.Current);
                i += 54;

                while (i != (storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock +
                    storage.INodeMap.UsedBlock + storage.INodeBlocks) * storage.Superblock.ClusterSize - 1)
                {
                    temp.WriteByte((byte)fs.ReadByte());
                    ++i;
                }

                byte[] arr = new byte[iNode.Size];
                for (int j = 0; j < arr.Length; ++j)
                    arr[j] = 1;

                temp.Write(arr, 0, arr.Length);
                fs.Seek(arr.Length, System.IO.SeekOrigin.Current);
                i += arr.Length;

                while (i != 62914560)
                {
                    temp.WriteByte((byte)fs.ReadByte());
                    ++i;
                }
            }
            return true;
        }

        //Number of blocks used to store only file data (without blocks addresses)
        private int BlocksForFile(Superblock superblock, int filesize)
        {
            return (int)Math.Ceiling((double)filesize / superblock.ClusterSize);
        }

        //Returnes short arr with blocks addresses
        private short[] GetBlocksArr(Bitmap bitmap, int blockNum)
        {
            var blockList = new List<short>() ;
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
        private int GetBlocksNum(int blocksNum, Superblock superblock)
        {
            int blocksNeeded = 0;
            if (blocksNum <= 13) // 13 is length of di_addr[] array
            {
                blocksNeeded = blocksNum;
            }
            else
            {
                int blockCapacity = superblock.ClusterSize / 2; //How many adresses of short type can be stored in one block
                blocksNeeded = blocksNum + (int)Math.Ceiling((double)blocksNum / blockCapacity); //Need to be edited cause can use one unnecessary block
                //blocksNeeded = blocks used to store fileData + blocks used to store blocks adresses
            }
            return blocksNeeded;
        }

        //Writes the file data + blocks addresses to short[]
        //I dont use real file data. Instead I just fill the blocks for file data with '1'
        //You can change 'filesize' to 'filedata'
        private void WriteFileData(short[] freeBlocks)
        {
        }

        public void AddFile(INode iNode)
        {
            WriteFile(DataExtractor.GetData(Path), iNode); 
        }
    }
}
