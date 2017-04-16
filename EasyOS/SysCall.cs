using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class FileSystem  //must be namespace EasyOS
{
    class SysCall
    {
        public static void Open(string path, FileSystem.FileAccess access)
        {


            string[] stringDir = new string[3];//must writeing 
            string fileName; //must writeing

            for (int i = 0; i < stringDir.Length; i++)
            {
                int currentDirId = InodeDirNameAndIDTable.GetInodeDirID(stringDir[i]);
                //seacrhc now

                InodeDir d = new InodeDir((byte)currentDirId);
                


            }
        }

       

        public static void Creat(string path) { }
        public static void Write() { }
        public static void Read() { }
    }
}
