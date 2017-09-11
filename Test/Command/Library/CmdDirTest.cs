// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Library;
using DosBoxTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Command.Library
{
    [TestClass]
    public class CmdDirTest : CmdTest
    {
        [TestInitialize]
        public void setUp()
        {
            // Check this filestructure in base class: crucial to understand the tests.
            this.CreateTestFileStructure();

            // Add all commands which are necessary to execute this unit test
            // Important: Other commands are not available unless added here.
            commandInvoker.AddCommand(new CmdDir("dir", drive));
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintPathOfCurrentDirectory()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains(rootDir.Path, testOutput);
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintFiles()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains(fileInRoot1.Name, testOutput);
            TestHelper.AssertContains(fileInRoot2.Name, testOutput);
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintDirectories()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains(subDir1.Name, testOutput);
            TestHelper.AssertContains(subDir2.Name, testOutput);
        }

        [TestMethod]
        public void CmdDir_WithoutParameter_PrintsFooter()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir");
            TestHelper.AssertContains("2 File(s)", testOutput);
            TestHelper.AssertContains("2 Dir(s)", testOutput);
        }

        [TestMethod]
        public void CmdDir_PathAsParameter_PrintGivenPath()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir c:\\subDir1");
            TestHelper.AssertContains(subDir1.Path, testOutput);
        }

        [TestMethod]
        public void CmdDir_PathAsParameter_PrintFilesInGivenPath()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir c:\\subDir1");
            TestHelper.AssertContains(file1InDir1.Name, testOutput);
            TestHelper.AssertContains(file2InDir1.Name, testOutput);
        }

        [TestMethod]
        public void CmdDir_PathAsParameter_PrintsFooter()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir c:\\subDir1");
            TestHelper.AssertContains("2 File(s)", testOutput);
            TestHelper.AssertContains("0 Dir(s)", testOutput);
        }

        [TestMethod]
        public void CmdDir_FileAsParameter_PrintGivenPath()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir " + this.file1InDir1.Path);
            TestHelper.AssertContains(subDir1.Path, testOutput);
        }

        [TestMethod]
        public void CmdDir_FileAsParameter_PrintFilesInGivenPath()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir " + this.file1InDir1.Path);
            TestHelper.AssertContains(file1InDir1.Name, testOutput);
            TestHelper.AssertContains(file2InDir1.Name, testOutput);
        }

        [TestMethod]
        public void CmdDir_FileAsParameter_PrintsFooter()
        {
            drive.ChangeCurrentDirectory(rootDir);
            ExecuteCommand("dir " + this.file1InDir1.Path);
            TestHelper.AssertContains("2 File(s)", testOutput);
            TestHelper.AssertContains("0 Dir(s)", testOutput);
        }

        [TestMethod]
        public void CmdDir_NotExistingDirectory_PrintsError()
        {
            ExecuteCommand("dir NotExistingDirectory");
            TestHelper.AssertContainsNoCase("File Not Found", this.testOutput);
        }

        [TestMethod]
        public void CmdDir_AllParametersAreReset()
        {
            drive.ChangeCurrentDirectory(subDir1);
            ExecuteCommand("dir c:\\subDir2");
            TestHelper.AssertContains(subDir2.Path, testOutput);
            this.testOutput.Empty();
            ExecuteCommand("dir");
            TestHelper.AssertContains(subDir1.Path, testOutput);
        }
    }
}