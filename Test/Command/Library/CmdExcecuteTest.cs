// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Library;
using DosBox.Filesystem;
using DosBoxTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Command.Library
{
    [TestClass]
    public class CmdExcecuteTest : CmdTest
    {
        [TestInitialize]
        public void SetUp()
        {
            // Check this filestructure in base class: crucial to understand the tests.
            this.CreateTestFileStructure();

            // Add all commands which are necessary to execute this unit test
            // Important: Other commands are not available unless added here.
            commandInvoker.AddCommand(new CmdDir("dir", drive));
        }

        [TestMethod]
        public void CmdExcecuteTest_ExcecuteCommandWithoutParameters()
        {
            Assert.IsTrue(ExecuteCommand("dir"));
        }

        [TestMethod]
        public void CmdExcecuteTest_ExcecuteCommandWithParameters()
        {
            Assert.IsTrue(ExecuteCommand("dir c:\\subDir1"));
        }

        [TestMethod]
        public void CmdExcecuteTest_ExcecuteCommandWithWrongParameters()
        {
            Assert.IsFalse(ExecuteCommand("dir @1234 + -"));
        }

        [TestMethod]
        public void CmdExcecuteTest_ExcecuteUnknownCommandWithoutParameters()
        {
            Assert.IsFalse(ExecuteCommand("asdf"));
        }
    }
}