using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1 //To change it completely
{
    class OffsetHandbook
    {
        public enum posGuide : long
        {
            SUPERBLOCK = 0,
            BITMAP = 4096,
            IMAP = 8192,
            INODES = 12288,
            MAINDIR = 841728,
        }

        public enum sizeGuide : int
        {
            FILEINDIR = 20,
            INODE = 54,
            GROUP = 20,
            USER = 24
        }

        public static long GetPos(posGuide pos)
        {
            return (long)pos;
        }

        public static int GetOffs(sizeGuide offs)
        {
            return (int)offs;
        }
    }
}
