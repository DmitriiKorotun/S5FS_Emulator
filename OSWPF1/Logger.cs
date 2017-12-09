using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSWPF1
{
    class Logger : IDisposable
    {
        private static Logger logger;

        private Logger(string newPath)
        {
            Logpath = newPath;
            Logs = new List<string>();
        }

        public static Logger GetInstance(string logpath)
        {
            if (logger == null)
                logger = new Logger(logpath);
            return logger;
        }

        List<string> logs;
        public List<string> Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        string logpath;
        public string Logpath
        {
            get { return logpath; }
            set { logpath = value; }
        }

        public void Log(string message)
        {
            Logs.Add(message);
        }

        void WriteToFile()
        {
            UnicodeEncoding unicode = new UnicodeEncoding();
            Thread.Sleep(200);
            using (var logstream = System.IO.File.OpenWrite(Logpath))
            {
                for (int i = 0; i < Logs.Count; ++i)
                    logstream.Write(unicode.GetBytes(Logs[i]), 0, unicode.GetByteCount(Logs[i]));
            }
        }

        public void Dispose()
        {
            if (Logs.Count != 0 && Logpath != null)
            {
                WriteToFile();
            }
        }
    }
}
