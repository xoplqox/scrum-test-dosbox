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
    }
}
