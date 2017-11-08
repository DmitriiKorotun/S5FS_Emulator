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

                    for (int i = 0; i < 62914560 - 16; ++i)
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

            byte[] fsType = new byte[4];
            for (int i = 0; i < 4; ++i)
                fsType[i] = (byte)fs.ReadByte();
            superblock.FSType = BitConverter.ToInt32(fsType, 0);

            byte[] iNodeCount = new byte[2];
            for (int i = 0; i < 2; ++i)
                iNodeCount[i] = (byte)fs.ReadByte();
            superblock.INodeCount = BitConverter.ToInt16(iNodeCount, 0);

            byte[] iNodeSize = new byte[2];
            for (int i = 0; i < 2; ++i)
                iNodeSize[i] = (byte)fs.ReadByte();
            superblock.INodeSize = BitConverter.ToInt16(iNodeSize, 0);

            byte[] freeBlock = new byte[2];
            for (int i = 0; i < 2; ++i)
                freeBlock[i] = (byte)fs.ReadByte();
            superblock.FreeBlock = BitConverter.ToInt16(freeBlock, 0);

            byte[] freeINode = new byte[2];
            for (int i = 0; i < 2; ++i)
                freeINode[i] = (byte)fs.ReadByte();
            superblock.FreeINode = BitConverter.ToInt16(freeINode, 0);
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

        void GetINodeMap(INodeMap iNodeMap, System.IO.FileStream fs, Superblock superblock)
        {
            int count = 0, i = 0;
            while (iNodeMap.NodeAdress[iNodeMap.NodeAdress.Length - 1] != 0 ||
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
        }

        public void GetData(FileDataStorage storage)
        {
            var fs = System.IO.File.OpenRead(Path);
            GetSuperblock(storage.Superblock, fs);
        }
    }
}
