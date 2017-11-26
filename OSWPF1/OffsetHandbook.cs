using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1 //To change it completely
{
    class OffsetHandbook
    {
        public enum offsetGuide : long
        {
            SUPERBLOCK = 0,
            BITMAP = 4096,
            IMAP = 8192,
            INODES = 12288,
            MAINDIR = 841728,
        }

        public static int GetSuperblockStart()
        {
            return 0;
        }

        public static int GetBitmapStart()
        {
            return 4096;
        }

        public static int GetIMapStart()
        {
            return 8192;
        }

        public static int GetNodesStart()
        {
            return 12288;
        }

        public static int GetMainDirStart()
        {
            return 841728;
        }
    }
}
