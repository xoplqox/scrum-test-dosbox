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

        public static void WriteInFile(string filename, string content)
        {
            try
            {

            
                if (!File.Exists(filename))
                {
                    CreateEmptyFile(filename);
                }

                // open the file and write.
                using (FileStream fs = File.OpenWrite(filename))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(content);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(filename))
                {
     
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        System.Console.Write(s);
                      
                    }
                }
            }

            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        private static object printToLog()
        {
            throw new NotImplementedException();
        }
    }
}
