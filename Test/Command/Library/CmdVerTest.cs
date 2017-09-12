using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DosBox.Command.Library;
using DosBox.Filesystem;
using DosBoxTest.Helpers;

namespace DosBoxTest.Command.Library
{
    [TestClass]
    public class CmdVerTest : CmdTest
    {
        [TestInitialize]
        public void setUp()
        {
            // Check this filestructure in base class: crucial to understand the tests.
            this.CreateTestFileStructure();
        }

        [TestMethod]
        public void CmdVer_NoParametersAreSet()
        {
            ExecuteCommand("ver");
            TestHelper.AssertContains("Microsoft Windows [Version 6.1.7601]", testOutput.ToString());
        }

        [TestMethod]
        public void CmdVer_WithParameterWSet()
        {
            ExecuteCommand("ver /w");
            TestHelper.AssertContains("Microsoft Windows [Version 6.1.7601]", testOutput.ToString());
            TestHelper.AssertContains("Hans", testOutput.ToString());
            TestHelper.AssertContains("Ahmend", testOutput.ToString());
            TestHelper.AssertContains("Caglar", testOutput.ToString());
            TestHelper.AssertContains("Frowin", testOutput.ToString());
            TestHelper.AssertContains("Axel", testOutput.ToString());
        }
    }
}