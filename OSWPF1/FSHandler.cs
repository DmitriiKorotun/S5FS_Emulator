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

        private long GetBlockEnd(int blockSize, long position)
        {
            long blockEndAddr = position, remDiv = blockEndAddr % blockSize, diff = blockSize - remDiv;
            //while (blockEndAddr % blockSize != 0)
            //{
            //    ++blockEndAddr;
            //}
            return blockEndAddr + diff;
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
                temp.SetLength(62914560);
                int freeBlock = DataExtractor.GetFreeBlock(storage.Bitmap), nodeNum = DataExtractor.GetINodeNum(storage.INodeMap);

                DataWriter.WriteSuperblock(temp, storage.Superblock);
                DataWriter.CopyData(fs, temp, temp.Position, GetBlockEnd(storage.Superblock.ClusterSize, temp.Position));
                var blocks = BlocksHandler.GetBlocksArr(storage.Bitmap, iNode.Size, storage.Superblock.ClusterSize);

                for (int i = 0; i < blocks.Length; ++i)
                {
                    storage.Bitmap.BitmapValue = BitWorker.TurnBitOn(storage.Bitmap.BitmapValue, blocks[i]);
                }
                //Here will be work with Bitmap

                DataWriter.WriteBitmap(temp, storage.Bitmap);
                DataWriter.CopyData(fs, temp, temp.Position, GetBlockEnd((storage.Superblock.UsedBlock +
                    storage.Bitmap.UsedBlock) * storage.Superblock.ClusterSize, temp.Position));

                //Here will be work with iNodeMap
                storage.INodeMap.BitmapValue = BitWorker.TurnBitOn(storage.INodeMap.BitmapValue, nodeNum);

                //DataWriter.WriteINodeMap(temp, storage.INodeMap);
                DataWriter.WriteBitmap(temp, storage.INodeMap);
                DataWriter.CopyData(fs, temp, temp.Position, GetBlockEnd((storage.Superblock.UsedBlock +
                    storage.Bitmap.UsedBlock + storage.INodeMap.UsedBlock) * storage.Superblock.ClusterSize, temp.Position));

                var dataDict = FileWriter.GetDataArr(blocks, storage.Superblock.ClusterSize);
                List<int> dataKeys = new List<int>(dataDict.Keys);

                for (int index = 0; index < iNode.Di_addr.Length && index < dataDict.Count; ++index)
                {
                    iNode.Di_addr[index] = (short)dataKeys[index]; //To change short for int cause data can be lost
                }
                long bytesNum = (nodeNum - 1) * 54; //How many bytes method need to skip
                //To change: make size dynamic
                DataWriter.CopyBytes(fs, temp, temp.Position, bytesNum);
                DataWriter.WriteINode(temp, iNode);
                DataWriter.CopyData(fs, temp, temp.Position, (storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock +
                    storage.INodeMap.UsedBlock + storage.INodeBlocks) * storage.Superblock.ClusterSize - 1);



                for (int i = 0; i < storage.Bitmap.BitmapValue.Length * 8; ++i)
                {
                    if (dataKeys.Contains(i))
                    {
                        DataWriter.WriteBlock(temp, storage.Superblock.ClusterSize, dataDict[i]);
                    }
                    else
                    {
                        DataWriter.CopyBytes(fs, temp, temp.Position, storage.Superblock.ClusterSize);
                    }
                }
                return true;
            }
        }


        // fs.Seek(blocks.Length, System.IO.SeekOrigin.Current);
        // SuperblockWriter.CopyBytes(fs, temp, temp.Position, 1);



        //    DataWriter.CopyData(fs, temp, position, (storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock) *
        //        storage.Superblock.ClusterSize + nodeNum - 1);
        //    position = (storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock) *
        //        storage.Superblock.ClusterSize + nodeNum - 1;

        //    DataWriter.CopyBytes(fs, temp, position, 1);
        //    ++position;

        //    while (position != (storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock +
        //        storage.INodeMap.UsedBlock) * storage.Superblock.ClusterSize + nodeNum - 1)
        //    {
        //        temp.WriteByte((byte)fs.ReadByte());
        //        ++position;
        //    }

        //    var iNodeArr = new INode[] {iNode };
        //    DataWriter.WriteINodes(temp, iNodeArr);

        //    fs.Seek(54, System.IO.SeekOrigin.Current);
        //    position += 54;

        //    while (position != (storage.Superblock.UsedBlock + storage.Bitmap.UsedBlock +
        //        storage.INodeMap.UsedBlock + storage.INodeBlocks) * storage.Superblock.ClusterSize - 1)
        //    {
        //        temp.WriteByte((byte)fs.ReadByte());
        //        ++position;
        //    }

        //    byte[] arr = new byte[iNode.Size];
        //    for (int j = 0; j < arr.Length; ++j)
        //        arr[j] = 1;

        //    temp.Write(arr, 0, arr.Length);
        //    fs.Seek(arr.Length, System.IO.SeekOrigin.Current);
        //    position += arr.Length;

        //    while (position != 62914560)
        //    {
        //        temp.WriteByte((byte)fs.ReadByte());
        //        ++position;
        //    }
        //}
        //return true;




        public void AddFile(INode iNode)
        {
            WriteFile(DataExtractor.GetData(Path), iNode);
        }
    }
}
