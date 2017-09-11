// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using System.Linq;
using DosBox.Filesystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Filesystem
{
    [TestClass]
    public class DirectoryTest
    {
        private Directory rootDir;
        private Directory subDir1;
        private Directory subDir2;
        private File file1InSubDir1;
        private File file2InSubDir1;

        [TestInitialize]
        public void setUp()
        {
            this.rootDir = new Directory("root");
            this.subDir1 = new Directory("subDir1");
            rootDir.Add(this.subDir1);
            this.subDir2 = new Directory("subDir2");
            rootDir.Add(this.subDir2);
            this.file1InSubDir1 = new File("file1InSubDir1", "content1");
            subDir1.Add(this.file1InSubDir1);
            this.file2InSubDir1 = new File("file2InSubDir1", "content2");
            subDir1.Add(this.file2InSubDir1);
        }

        [TestMethod]
        public void Constructor_CreateWithName_IsCorrectlyCreated()
        {
            string name = "root";
            var testdir = new Directory(name);
            Assert.AreEqual(name, testdir.Name);
            Assert.AreEqual(0, testdir.GetNumberOfContainedDirectories());
            Assert.AreEqual(0, testdir.GetNumberOfContainedFiles());
            Assert.IsNull(testdir.Parent);
        }

        [TestMethod]
        public void Content_Subdirectory_RootContainsValidData_BaseForNextTests()
        {
            IEnumerable<FileSystemItem> content = rootDir.Content;
            Assert.IsNotNull(content);
            Assert.AreEqual(2, content.Count());
        }

        [TestMethod]
        public void Content_Subdirectory_Element0HasCorrectName()
        {
            FileSystemItem item = rootDir.Content.ElementAt(0);
            Assert.IsTrue(item.IsDirectory());
            Assert.AreEqual(subDir1.Name, item.Name);
            Assert.AreSame(this.subDir1, item);
        }

        [TestMethod]
        public void Content_Subdirectory_ParentIsSetCorrectly()
        {
            FileSystemItem item = rootDir.Content.ElementAt(0);
            FileSystemItem parent = item.Parent;
            Assert.IsTrue(parent.IsDirectory());
            Assert.AreSame(this.rootDir, parent);
        }

        [TestMethod]
        public void Content_Subdirectory_ParentIsSetCorrectly1()
        {
            FileSystemItem item = rootDir.Content.ElementAt(0);
            FileSystemItem parent = item.Parent;
            Assert.IsNotNull(parent.Parent == null);
            string path = item.Path;
            Assert.IsTrue(path.CompareTo(rootDir.Name + "\\" + subDir1.Name) == 0);
        }

        [TestMethod]
        public void testContainingFiles()
        {
            FileSystemItem dir = rootDir.Content.ElementAt(0);
            FileSystemItem file1 = dir.Content.ElementAt(0);
            Assert.IsNotNull(file1);
            Assert.AreEqual(this.file1InSubDir1, file1);
        }

        [TestMethod]
        public void testForDirectory()
        {
            Assert.IsTrue(rootDir.IsDirectory());
            Assert.IsTrue(subDir2.IsDirectory());
        }

        [TestMethod]
        public void testRename()
        {
            subDir1.Name = "NewName";
            Assert.IsTrue(subDir1.Name.CompareTo("NewName") == 0);
        }

        [TestMethod]
        public void testNumberOfFilesAndDirectories()
        {
            // TODO
        }
    }
}