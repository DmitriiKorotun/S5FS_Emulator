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

        void GetSuperblock(Superblock superblock, System.IO.FileStream fs)
        {
            superblock.ClusterSize = ShortFromBytes(fs, 2);
            superblock.FSType = IntFromBytes(fs, 4);
            superblock.INodeCount = ShortFromBytes(fs, 2);
            superblock.INodeSize = ShortFromBytes(fs, 2);
            superblock.FreeBlock = ShortFromBytes(fs, 2);
            superblock.FreeINode = ShortFromBytes(fs, 2);
        }

        short ShortFromBytes(System.IO.FileStream fs, int byteArrSize)
        {
            byte[] byteArr = new byte[byteArrSize];
            for (int i = 0; i < byteArrSize; ++i)
                byteArr[i] = (byte)fs.ReadByte();
            return BitConverter.ToInt16(byteArr, 0);
        }

        int IntFromBytes(System.IO.FileStream fs, int byteArrSize)
        {
            byte[] byteArr = new byte[byteArrSize];
            for (int i = 0; i < byteArrSize; ++i)
                byteArr[i] = (byte)fs.ReadByte();
            return BitConverter.ToInt32(byteArr, 0);
        }

        void GetBitmap(Bitmap bitmap, System.IO.FileStream fs)
        {
            for (int i = 0; i < bitmap.BitmapValue.Length; ++i)
                bitmap.BitmapValue[i] = (byte)fs.ReadByte();
        }

        bool GetINodeMap(INodeMap iNodeMap, System.IO.FileStream fs, Superblock superblock)
        {
            int count = 0, i = 0;
            while (iNodeMap.NodeAdress[iNodeMap.NodeAdress.Length - 1] != 0 &&
                count < superblock.INodeCount)
            {
                byte iNode = (byte)fs.ReadByte();
                if (iNode == 0)
                {
                    iNodeMap.NodeAdress[i] = (short)count;
                    ++i;
                }
                ++count;
            }
            bool isNodeLeft = count < superblock.INodeCount ? true : false;
            return isNodeLeft;
        }

        public FileDataStorage GetData()
        {
            var storage = new FileDataStorage();
            using (var fs = System.IO.File.OpenRead(Path))
            {
                GetSuperblock(storage.Superblock, fs);
                fs.Seek(4096, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
                GetBitmap(storage.Bitmap, fs);
                fs.Seek(4096 * 2, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
                GetINodeMap(storage.INodeMap, fs, storage.Superblock);
                fs.Seek(4096 * 6, System.IO.SeekOrigin.Begin); //Change offset to be dynamic
            }
            return storage;
        }

        //public void CreateUserGroup(long offset, System.IO.FileStream fs)
        //{
        //    fs.Seek(offset, System.IO.SeekOrigin.Begin);
        //    fs.Write()
        //}

        private bool WriteFile(FileDataStorage storage, INode iNode)
        {
            using (var fs = System.IO.File.OpenRead(Path))
            {
                int freeBlock = GetFreeBlock(storage.Bitmap);
                int nodeNum = GetINode(storage, fs);
                var temp = new System.IO.FileStream(fs.Name + ".temp", System.IO.FileMode.Create);
                fs.Seek(0, System.IO.SeekOrigin.Begin);
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
                fs.Seek(22, System.IO.SeekOrigin.Current);
                i += 22;
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

        private int GetFreeBlock(Bitmap blockMap)
        {
            int i = 0, blockNum = -1;
            bool isFound = false;
            while (i < blockMap.BitmapValue.Length && !isFound)
            {
                if (blockMap.BitmapValue[i] == 0)
                {
                    blockNum = i + 1;
                    isFound = true;
                }
                ++i;
            }
            return blockNum;
        }

        private int GetINodeNum(INodeMap nodeMap)
        {
            int adress = -1;
            for (int i = 0; i < nodeMap.NodeAdress.Length; ++i)
            {
                if (nodeMap.NodeAdress[i] == 0)
                {
                    nodeMap.NodeAdress[i] = 1;
                    adress = i + 1;
                    break;
                }
            }
            return adress;
        }

        private int GetINode(FileDataStorage storage, System.IO.FileStream fs)
        {
            int num = GetINodeNum(storage.INodeMap);
            if (num < 0)
            {
                GetINodeMap(storage.INodeMap, fs, storage.Superblock);
                num = GetINodeNum(storage.INodeMap);              
            }
            return num;
        }

        public void AddFile(INode iNode)
        {
            WriteFile(GetData(), iNode); 
        }
    }
}
