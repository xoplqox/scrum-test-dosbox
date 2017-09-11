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
    public class CmdMkDirTest : CmdTest
    {
        [TestInitialize]
        public void setUp()
        {
            // Check this filestructure in base class: crucial to understand the tests.
            this.CreateTestFileStructure();

            // Add all commands which are necessary to execute this unit test
            // Important: Other commands are not available unless added here.
            commandInvoker.AddCommand(new CmdMkDir("mkdir", drive));
        }

        [TestMethod]
        public void CmdMkDir_CreateNewDirectory_NewDirectoryIsAdded()
        {
            const string testDirName = "test1";
            ExecuteCommand("mkdir " + testDirName);
            Directory testDirectory = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveLetter, testDirName),
                                                              testDirName);
            Assert.AreSame(drive.RootDirectory, testDirectory.Parent);
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 1, drive.RootDirectory.GetNumberOfContainedDirectories());
            TestHelper.AssertOutputIsEmpty(testOutput);
        }

        [TestMethod]
        public void CmdMkDir_CreateNewDirectory_NewDirectoryIsAddedToCorrectLocation()
        {
            const string testDirName = "test1";
            ExecuteCommand("mkdir " + testDirName);
            Directory testDirectory = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveLetter, testDirName),
                                                              testDirName);
            Assert.AreSame(drive.RootDirectory, testDirectory.Parent);
        }

        [TestMethod]
        public void CmdMkDir_SingleLetterDirectory_NewDirectoryIsAdded()
        {
            const string testDirName = "a";
            ExecuteCommand("mkdir " + testDirName);
            Directory testDirectory = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveLetter, testDirName),
                                                              testDirName);
            Assert.AreSame(drive.RootDirectory, testDirectory.Parent);
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 1, drive.RootDirectory.GetNumberOfContainedDirectories());
            TestHelper.AssertOutputIsEmpty(testOutput);
        }

        [TestMethod]
        public void CmdMkDir_NoParameters_ErrorMessagePrinted()
        {
            ExecuteCommand("mkdir");
            Assert.AreEqual(numbersOfDirectoriesBeforeTest, drive.RootDirectory.GetNumberOfContainedDirectories());
            TestHelper.AssertContains("syntax of the command is incorrect", testOutput);
        }

        [TestMethod]
        public void CmdMkDir_ParameterContainsBacklash_ErrorMessagePrinted()
        {
            ExecuteCommand("mkdir c:\\test1");
            TestHelper.AssertContains("At least one parameter", this.testOutput);
            TestHelper.AssertContains("path rather than a directory name", this.testOutput);
        }

        [TestMethod]
        public void CmdMkDir_ParameterContainsBacklash_NoDirectoryCreated()
        {
            ExecuteCommand("mkdir c:\\test1");
            Assert.AreEqual(numbersOfDirectoriesBeforeTest, drive.RootDirectory.GetNumberOfContainedDirectories());
        }

        [TestMethod]
        public void CmdMkDir_SeveralParameters_SeveralNewDirectoriesCreated()
        {
            // given
            const string testDirName1 = "test1";
            const string testDirName2 = "test2";
            const string testDirName3 = "test3";

            // when
            ExecuteCommand("mkdir " + testDirName1 + " " + testDirName2 + " " + testDirName3);

            // then
            Directory directory1 = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveLetter, testDirName1),
                                                           testDirName1);
            Directory directory2 = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveLetter, testDirName2),
                                                           testDirName2);
            Directory directory3 = TestHelper.GetDirectory(drive, Path.Combine(drive.DriveLetter, testDirName3),
                                                           testDirName3);
            Assert.AreSame(directory1.Parent, drive.RootDirectory);
            Assert.AreSame(directory2.Parent, drive.RootDirectory);
            Assert.AreSame(directory3.Parent, drive.RootDirectory);
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 3, drive.RootDirectory.GetNumberOfContainedDirectories());
            TestHelper.AssertOutputIsEmpty(testOutput);
        }

        [TestMethod]
        public void CmdMkDir_AllParametersAreReset()
        {
            const string testDirName = "test1";
            ExecuteCommand("mkdir " + testDirName);
            System.Console.WriteLine(testOutput.ToString());
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 1, drive.RootDirectory.GetNumberOfContainedDirectories());

            ExecuteCommand("mkdir");
            Assert.AreEqual(numbersOfDirectoriesBeforeTest + 1, drive.RootDirectory.GetNumberOfContainedDirectories());
            TestHelper.AssertContains("The syntax of the command is incorrect", this.testOutput);
        }
    }
}