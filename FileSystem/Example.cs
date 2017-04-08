using System;
using Hardware;
using FileSystem;
namespace Example
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //instal
            byte[] buffer = BitConverter.GetBytes(4096);//blockSize
            HDD.Write(buffer, 0);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(1024);//blockCountInInodes
            HDD.Write(buffer, 4);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(1024*128);//maxBlocks
            HDD.Write(buffer, 8);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(0);//blockStart
            HDD.Write(buffer, 12);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(1024 * 128 - 1);//freeBlocksCount
            HDD.Write(buffer, 16);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(4096 * 128 * 1024);//inodeStart
            HDD.Write(buffer, 20);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(128);//maxInodesCount
            HDD.Write(buffer, 24);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(0);//currentInodesCount
            HDD.Write(buffer, 28);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(1032);//inodeSize
            HDD.Write(buffer, 32);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(1024 * 128);//freeSpaceMgmtSize
            HDD.Write(buffer, 40);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(1032 * 128 + (4096 * 1024 * 128));//freeSpaceMgmtStart
            HDD.Write(buffer, 36);
            while (!HDD.isNullWriteHandler()) ;
          
            buffer = BitConverter.GetBytes(1024 * 128 + (1032 * 128 + (4096 * 1024 * 128)));//root
            HDD.Write(buffer, 44);
            while (!HDD.isNullWriteHandler()) ;

            buffer = BitConverter.GetBytes(1024 * 128 + (1032 * 128 + (4096 * 1024 * 128)));//inodeTableStart
            HDD.Write(buffer, 48);
            while (!HDD.isNullWriteHandler()) ;
            
            Console.WriteLine(SuperBlock.InodeTableStart);

        }
    }
}


//-------------------instal




//--------------------test
//Clock.SetTimer();
//int intValue = 82;
//byte[] intValueOnByteArray = BitConverter.GetBytes(intValue);

//HDD.Write(intValueOnByteArray, 25);

//intValue = 19;
////intValueOnByteArray = BitConverter.GetBytes(intValue);
////HDD.Write(intValueOnByteArray, 24);


//byte[] h = new byte[40];

//byte[] stringOnBytes;

//string s = "hes1a ";

//for (int i = 0; i < s.Length; i++)
//{
//    stringOnBytes = BitConverter.GetBytes(s[i]);
//    HDD.Write(stringOnBytes, i);
//}

//HDD.Read(ref h, 0, 40);



//while (!HDD.isNullReadHandler()) ;
//for (int i = 0; i < h.Length; i++)
//{
//    Console.Write(i); Console.WriteLine("  " + h[i]);
//}
