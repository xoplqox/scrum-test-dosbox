using DosBoxTest.Helpers;
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
          /*
            string fileName = "testLog";
            string content = "test writing test";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            DosBox.trace.TraceUtils.WriteInFile(fileName, content);

            String newContent = "";
            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(fileName))
            {
                
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    newContent += s;

                }
            }
            TestHelper.AssertContains(content, newContent);
           * 
         */
        }
    }
}
