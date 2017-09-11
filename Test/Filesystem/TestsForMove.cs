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
    public class TestsForMove : FileSystemTestCase
    {
        [TestInitialize]
        public override void setUp()
        {
            base.setUp();
        }

        [TestMethod]
        public void testFileMove()
        {
            IEnumerable<FileSystemItem> content;

            // Check Preconditions
            Assert.IsTrue(file1InDir1.Path.CompareTo("C:\\subDir1\\File1InDir1") == 0);
            Assert.AreSame(file1InDir1.Parent, subDir1);
            content = subDir2.Content;
            Assert.IsTrue(content.Contains(file1InDir1) == false);
            content = subDir1.Content;
            Assert.IsTrue(content.Contains(file1InDir1));

            // Do move
            subDir2.Add(file1InDir1);

            // Check Postconditions
            Assert.IsTrue(file1InDir1.Path.CompareTo("C:\\subDir2\\File1InDir1") == 0);
            Assert.AreSame(file1InDir1.Parent, subDir2);
            content = subDir2.Content;
            Assert.IsTrue(content.Contains(file1InDir1));
            content = subDir1.Content;
            Assert.IsTrue(content.Contains(file1InDir1) == false);
        }
    }
}