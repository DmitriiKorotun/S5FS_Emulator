using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSWPF1
{
    class FileDataStorage
    {
        Superblock superblock;
        public Superblock Superblock
        {
            get { return superblock; }
        }

        Bitmap bitmap;
        public Bitmap Bitmap
        {
            get { return bitmap; }
        }

        Bitmap iNodeMap;
        public Bitmap INodeMap
        {
            get { return iNodeMap; }
        }

        int iNodeBlocks;
        public int INodeBlocks
        {
            get { return 207; }
        }

        public FileDataStorage()
        {
            superblock = new Superblock();
            bitmap = new Bitmap();
            iNodeMap = new Bitmap();
        }
    }
}
