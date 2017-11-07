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
                using (Logger.GetInstance(path + ".log"))
                {
                    System.IO.FileStream fs = null;
                    try
                    {
                        fs = System.IO.File.Create(path);
                        Logger.GetInstance(null).Log(DateTime.Now + " - MainFile was created with name: " + path
                            + Environment.NewLine);
                        for (long i = 0; i < 1024; ++i) //62914560
                        {
                            fs.WriteByte(48);
                        }
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
            Path = path;
        }

        void GetSuperblock(Superblock superblock, System.IO.FileStream fs)
        {
            byte[] clusterSize = new byte[2];
            for (int i = 0; i < 2; ++i)
                clusterSize[i] = (byte)fs.ReadByte();
            superblock.ClusterSize = BitConverter.ToInt16(clusterSize, 0);

            byte[] fsType = new byte[4];
         //   fs.Seek(2, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < 4; ++i)
                fsType[i] = (byte)fs.ReadByte();
            superblock.FSType = BitConverter.ToInt32(fsType, 0);
        }

        public void GetData(FileDataStorage storage)
        {
            var fs = System.IO.File.OpenRead(Path);
            GetSuperblock(storage.Superblock, fs);
            if (fs != null)
                return;
        }
    }
}
