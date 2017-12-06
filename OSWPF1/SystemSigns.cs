using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class SystemSigns
    {
        public enum Signs : int
        {
            CREATEMAINDIR = 0,
            MAINDIRNODE = 1,
            CREATEUSERFILE = 255,
            CREATEGROUPFILE = 256,
            CREATEBANNEDFILE = 257,
            WRITE = 2000,
            READ = 4000,
            EX = 1000,
            USER = 1001,
            GROUP = 1002,
            OTHER = 1003
        };
    }
}
