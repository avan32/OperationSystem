using System;
using Hardware;


namespace FileSystem
{
    //starting on 0 position
    class SuperBlock
    {
        //blockSize
        private static int blockSize = -1;//4096
        public static int BlockSize
        {
            get
            {
                if (blockSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 0);
                    while (!HDD.isNullReadHandler()) ;

                    blockSize = BitConverter.ToInt32(buffer, 0);
                }
                return blockSize;
            }
        }

        //blockCountInInodes
        private static int blockCountInInodes = -1;//1024
        public static int BlockCountInInodes
        {
            get
            {
                if (blockCountInInodes == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 4);
                    while (!HDD.isNullReadHandler()) ;

                    blockCountInInodes = BitConverter.ToInt32(buffer, 0);
                }
                return blockCountInInodes;
            }
        }

        //maxBlocks
        private static int maxBlocks = -1;//1024*128
        public static int MaxBlocks
        {
            get
            {
                if (maxBlocks == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 8);
                    while (!HDD.isNullReadHandler()) ;

                    maxBlocks = BitConverter.ToInt32(buffer, 0);
                }
                return maxBlocks;
            }
        }

        //blockStart
        private static int blockStart = -1;//0
        public static int BlockStart
        {
            get
            {
                if (blockStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 12);
                    while (!HDD.isNullReadHandler()) ;

                    blockStart = BitConverter.ToInt32(buffer, 0);
                }
                return blockStart;
            }
        }

        //freeBlocksCount
        private static int freeBlocksCount = -1;//1024*128-1
        private static bool boolFreeBlockCount = false;
        public static int FreeBlocksCount
        {
            get
            {
                if (!boolFreeBlockCount)
                {
                    boolFreeBlockCount = true;
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 16);
                    while (!HDD.isNullReadHandler()) ;

                    blockStart = BitConverter.ToInt32(buffer, 0);
                }
                return freeBlocksCount;
            }

            set
            {
                boolFreeBlockCount = false;
                freeBlocksCount = value;
                byte[] buffer = new byte[4];
                buffer = BitConverter.GetBytes(freeBlocksCount);
                HDD.Write(buffer, 16);
                while (!HDD.isNullWriteHandler()) ;

            }
        }

        //inodeStart
        private static int inodeStart = -1;//4096*128*1024 
        public static int InodeStart
        {
            get
            {
                if (inodeStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 20);
                    while (!HDD.isNullReadHandler()) ;

                    inodeStart = BitConverter.ToInt32(buffer, 0);
                }
                return inodeStart;
            }
        }

        //maxInodesCount
        private static int maxInodesCount = -1;//128
        public static int MaxInodesCount
        {
            get
            {
                if (maxInodesCount == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 24);
                    while (!HDD.isNullReadHandler()) ;

                    maxInodesCount = BitConverter.ToInt32(buffer, 0);
                }
                return maxInodesCount;
            }
        }

        //currentInodesCount
        private static int currentInodesCount = -1;
        private static bool boolCurrentInodesCount = false;
        public static int CurrentInodesCount
        {
            get
            {
                if (!boolCurrentInodesCount)
                {
                    boolCurrentInodesCount = true;
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 28);
                    while (!HDD.isNullReadHandler()) ;

                    currentInodesCount = BitConverter.ToInt32(buffer, 0);
                }
                return currentInodesCount;
            }
            set
            {
                boolCurrentInodesCount = false;
                byte[] buffer = new byte[4];
                buffer = BitConverter.GetBytes(value);
                HDD.Write(buffer, 28);
                while (!HDD.isNullReadHandler()) ;
            }
        }

        //inodeSize
        private static int inodeSize = -1; //1032
        public static int InodeSize
        {
            get
            {
                if (inodeSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 32);
                    while (!HDD.isNullReadHandler()) ;

                    inodeSize = BitConverter.ToInt32(buffer, 0);
                }
                return inodeSize;
            }
        }

        //freeSpaceMgmtStart
        private static int freeSpaceMgmtStart = -1;//1032*128 + (4096*1024*128 )
        public static int FreeSpaceMgmtStart
        {
            get
            {
                if (freeSpaceMgmtStart == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 36);
                    while (!HDD.isNullReadHandler()) ;

                    freeSpaceMgmtStart = BitConverter.ToInt32(buffer, 0);
                }
                return freeSpaceMgmtStart;
            }
        }

        //freeSpaceMgmtSize
        private static int freeSpaceMgmtSize = -1;//1024*128
        public static int FreeSpaceMgmtSize
        {
            get
            {
                if (freeSpaceMgmtSize == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 40);
                    while (!HDD.isNullReadHandler()) ;

                    freeSpaceMgmtSize = BitConverter.ToInt32(buffer, 0);
                }
                return freeSpaceMgmtSize;
            }
        }

        //root
        private static int root = -1;// 1024*128  +(1032*128 + (4096*1024*128 ))
        public static int Root
        {
            get
            {
                if (root == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, 44);
                    while (!HDD.isNullReadHandler()) ;

                    root = BitConverter.ToInt32(buffer, 0);
                }
                return root;
            }
        }

        public static int InodeTableStart;
    }

    class InodeTable
    {
        private static byte[] arrayID = new byte[128];
        public static byte GetArrayID(int index) {
            byte[] buffer = new byte[1];
            HDD.Read(ref buffer, SuperBlock.InodeTableStart + index);
            while (!HDD.isNullReadHandler()) ;
            arrayID[index] = buffer[0];
            return arrayID[index];
        }
        public static void SetArrayID(int index,byte value)
        {
            arrayID[index] = value;
            HDD.Write(new byte[1] { value }, SuperBlock.InodeTableStart + index);
            while (!HDD.isNullWriteHandler()) ;
        }
        public static byte GetID() {

            for (byte i = 0; i < arrayID.Length; i++)
                if (GetArrayID(i) == 255) return i; 
            return 0;
        }
    }

    class Inode
    {
        //ID > 128 error 
        public Inode(byte ID = 255) {
            if (ID == 255)
            {
                fileID = InodeTable.GetID();
            }
            else fileID = ID;
        }

        private byte[] arrayBlocks = new byte[1024];
        public byte GetArrayBlock(int index) {

            byte[] buffer = new byte[1];
            HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + index);
            arrayBlocks[index] = buffer[0];

            return arrayBlocks[index];
        }

        private byte fileID;

        private int blockCount = -1;
        public int BlockCount
        {
            get
            {
                if (blockCount == -1)
                {
                    byte[] buffer = new byte[4];
                    HDD.Read(ref buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 1024);
                    while (!HDD.isNullReadHandler()) ;

                    blockCount = BitConverter.ToInt32(buffer, 0);
                }
                return blockCount;
            }
            set
            {
                byte[] buffer = new byte[4];
                HDD.Write(buffer, SuperBlock.InodeStart + fileID * SuperBlock.InodeSize + 1024);
                while (!HDD.isNullWriteHandler()) ;

                blockCount = BitConverter.ToInt32(buffer, 0);
            }
        }

        public int modifyTime;
        public int accsesTime;

    }

    class FreeSpaceMgmt
    {
        //arrayBlock
        private static byte[] arrayBlock = new byte[SuperBlock.MaxBlocks];
        public static void SetArrayBlock(int index, byte value)
        {
            arrayBlock[index] = value;
            HDD.Write(new byte[1] { value }, SuperBlock.FreeSpaceMgmtStart + index);
            while (!HDD.isNullWriteHandler()) ;
        }

        public static byte GetArrayBlock(int index)
        {
            byte[] b = new byte[1];
            HDD.Read(ref b, SuperBlock.FreeSpaceMgmtStart + index);
            arrayBlock[index] = b[0];
            return arrayBlock[index];
        }
        public static int GetBlock()
        {

            //0 block is SuberBlock
            for (int i = 1; i < arrayBlock.Length; i++)
            {
                if (GetArrayBlock(i) == 0) return i;
            }

            //Get SuperBlock
            return 0;
        }

    }
}



namespace Example
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Clock.SetTimer();
            int intValue = 82;
            byte[] intValueOnByteArray = BitConverter.GetBytes(intValue);

            HDD.Write(intValueOnByteArray, 25);

            intValue = 19;
            //intValueOnByteArray = BitConverter.GetBytes(intValue);
            //HDD.Write(intValueOnByteArray, 24);


            byte[] h = new byte[40];

            byte[] stringOnBytes;

            string s = "hes1a ";

            for (int i = 0; i < s.Length; i++)
            {
                stringOnBytes = BitConverter.GetBytes(s[i]);
                HDD.Write(stringOnBytes, i);
            }

            HDD.Read(ref h, 0, 40);



            while (!HDD.isNullReadHandler()) ;
            for (int i = 0; i < h.Length; i++)
            {
                Console.Write(i); Console.WriteLine("  " + h[i]);
            }
        }
    }
}
