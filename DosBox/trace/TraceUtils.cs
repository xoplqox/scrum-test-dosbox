using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DosBox.trace
{
    public class TraceUtils
    {
        public static void CreateEmptyFile(string filename)
        {
            File.Create(filename).Dispose();
        }
        
    }
}
