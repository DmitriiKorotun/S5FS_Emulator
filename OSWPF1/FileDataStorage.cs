﻿using System;
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

        public FileDataStorage()
        {
            superblock = new Superblock();
            bitmap = new Bitmap();
        }
    }
}
