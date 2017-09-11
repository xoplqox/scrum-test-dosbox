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
    public class TestsForCmdDir : FileSystemTestCase
    {
        [TestInitialize]
        public override void setUp()
        {
            base.setUp();
        }

        [TestMethod]
        public void testContent()
        {
            IEnumerable<FileSystemItem> content;

            content = rootDir.Content;
            Assert.IsTrue(content.Count() == 4); // subDir1, subDir2, file1InRoot, file2InRoot
            Assert.IsTrue(content.Contains(subDir1));
            Assert.IsTrue(content.Contains(subDir2));
            Assert.IsTrue(content.Contains(fileInRoot1));
            Assert.IsTrue(content.Contains(fileInRoot2));
            Assert.IsTrue(content.Contains(file1InDir1) == false);

            content = subDir1.Content;
            Assert.IsTrue(content.Count() == 2); // file1InDir1, file2InDir1
            Assert.IsTrue(content.Contains(file1InDir1));
            Assert.IsTrue(content.Contains(file2InDir1));
            Assert.IsTrue(content.Contains(fileInRoot2) == false);

            content = subDir2.Content;
            Assert.IsTrue(content.Count() == 0);
        }
    }
}