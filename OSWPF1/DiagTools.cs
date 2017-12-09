using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Timers;

namespace OSWPF1
{
    class DiagTools
    {
        public static bool IsFileLocked(FileInfo file, bool ToRead)
        {
            FileStream stream = null;

            try
            {
                if (ToRead)
                    stream = file.OpenRead();
                else
                    stream = file.OpenWrite();
             //   stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                 
            }
            catch (IOException)
            {
                System.Threading.Thread.Sleep(3000);
                try
                {
                    if (ToRead)
                        stream = file.OpenRead();
                    else
                        stream = file.OpenWrite();
                    //stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(3000);
                    try
                    {
                        if (ToRead)
                            stream = file.OpenRead();
                        else
                            stream = file.OpenWrite();
                        //stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    }
                    catch (IOException)
                    {
                        return true;
                    }
                    //the file is unavailable because it is:
                    //still being written to
                    //or being processed by another thread
                    //or does not exist (has already been processed)
                    return false;
                }
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
