using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DosBoxTest.trace
{
    [TestClass]
    public class TraceUtilsTest
   
    {
        [TestMethod]
        public void CreateEmptyFile_SuccessTest()
        {
            string fileName = "testLog";
            DosBox.trace.TraceUtils.CreateEmptyFile(fileName);
            Assert.IsTrue(File.Exists(fileName));
        }

        [TestMethod]
        public void WriteInFileSuccessful_SuccessTest()
        {
            string fileName = "testLog";
            string content = "test writing \n test";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            DosBox.trace.TraceUtils.WriteInFile(fileName, content);

            // Open the stream and read it back.

            string s = "";
            using (StreamReader sr = File.OpenText(fileName))
            {
                
                while ((s += sr.ReadLine()) != null)
                {
                    

                }
            }
            Assert.Equals(content,s);
        }
    }
}
