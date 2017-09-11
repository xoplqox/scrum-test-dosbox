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
    public class CmdCdTest : CmdTest
    {
        [TestInitialize]
        public void SetUp()
        {
            // Check this file structure in base class: crucial to understand the tests.
            this.CreateTestFileStructure();

            // Add all commands which are necessary to execute this unit test
            // Important: Other commands are not available unless added here.
            commandInvoker.AddCommand(new CmdCd("cd", drive));
        }

        [TestMethod]
        public void CmdCd_ChangeToSubdirectory_ChangesDirectory()
        {
            ExecuteCommand("cd " + subDir1.Path);
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, subDir1);
        }

        [TestMethod]
        public void CmdCd_ChangeToSubDirectoryWithEndingBacklash_ChangesDirectory()
        {
            ExecuteCommand("cd " + subDir1.Path + "\\");
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, subDir1);
        }

        [TestMethod]
        public void CmdCd_WithBacklash_ChangesToRoot()
        {
            drive.ChangeCurrentDirectory(subDir1);
            ExecuteCommand("cd \\");
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, rootDir);
        }

        [TestMethod]
        public void CmdCd_WithPointPoint_ChangesToParent()
        {
            drive.ChangeCurrentDirectory(subDir1);
            ExecuteCommand("cd ..");
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, rootDir);
        }

        [TestMethod]
        public void CmdCd_WithPointPointInRootDir_RemainsInRootDir()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("cd ..");
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, rootDir);
        }

        [TestMethod]
        public void CmdCd_WithPoint_RemainsInCurrentDirectory()
        {
            drive.ChangeCurrentDirectory(subDir1);
            ExecuteCommand("cd .");
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, subDir1);
        }

        [TestMethod]
        public void CmdCd_WithPointInRootDir_RemainsInCurrentDirectory()
        {
            ExecuteCommand("cd .");
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, rootDir);
        }

        [TestMethod]
        public void CmdCd_WithoutParameter_PrintsCurrentDirectory()
        {
            drive.ChangeCurrentDirectory(subDir1);
            ExecuteCommand("cd");
            TestHelper.AssertContains(subDir1.Path, testOutput.ToString());
            TestHelper.AssertCurrentDirectoryIs(drive, subDir1);
        }

        [TestMethod]
        public void CmdCd_WithInvalidAbsolutePath_RemainsInCurrentDirectory()
        {
            drive.ChangeCurrentDirectory(subDir2);
            ExecuteCommand("cd c:\\gaga\\gugus");
            TestHelper.AssertCurrentDirectoryIs(drive, subDir2);
            TestHelper.AssertContains("system cannot find the path specified", testOutput.ToString());
        }

        [TestMethod]
        public void CmdCd_WithFileAsPath_RemainsInCurrentDirectory()
        {
            drive.ChangeCurrentDirectory(subDir2);
            ExecuteCommand("cd " + file1InDir1.Path);
            TestHelper.AssertCurrentDirectoryIs(drive, subDir2);
            TestHelper.AssertContains("directory name is invalid", testOutput.ToString());
        }

        [TestMethod]
        public void CmdCd_WithRelativePath_ChangesDirectory()
        {
            ExecuteCommand("cd " + subDir1.Name);
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, subDir1);
        }

        [TestMethod]
        public void CmdCd_WithSingleLetterDirectory_ChangesDirectory()
        {
            // given
            Directory directoryWithSingleLetter = new Directory("a");
            rootDir.Add(directoryWithSingleLetter);
            drive.ChangeCurrentDirectory(rootDir);

            // when
            ExecuteCommand("cd " + directoryWithSingleLetter.Name);

            // then
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, directoryWithSingleLetter);
        }

        [TestMethod]
        public void CmdCd_ChangeToNotExistingDirectory_PrintsError()
        {
            this.drive.ChangeCurrentDirectory(this.rootDir);
            ExecuteCommand("cd NotExistingDirectory");
            TestHelper.AssertCurrentDirectoryIs(drive, this.rootDir);
            TestHelper.AssertContainsNoCase("cannot find the path specified", this.testOutput);
        }

        [TestMethod]
        public void CmdCd_AllParametersAreReset()
        {
            ExecuteCommand("cd " + subDir1.Path);
            TestHelper.AssertOutputIsEmpty(testOutput);
            TestHelper.AssertCurrentDirectoryIs(drive, subDir1);
            ExecuteCommand("cd");
            TestHelper.AssertContains(subDir1.Path, testOutput.ToString());
        }
    }
}