// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using DosBox.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Command.Framework
{
    [TestClass]
    public class OutputterTest
    {
        private IOutputter outputter = new TestOutput();

        [TestMethod]
        public void HasCharactersPrinted_ByDefault_IsFalse()
        {
            Assert.IsFalse(outputter.HasCharactersPrinted());
            Assert.AreEqual(0, outputter.NumberOfCharactersPrinted());
        }

        [TestMethod]
        public void HasCharactersPrinted_PrintAString_IsTrue()
        {
            outputter.Print("a text");
            Assert.IsTrue(outputter.HasCharactersPrinted());
            Assert.AreEqual(6, outputter.NumberOfCharactersPrinted());
        }

        [TestMethod]
        public void HasCharactersPrinted_PrintSpaces_IsFalse()
        {
            outputter.Print("     ");
            Assert.IsFalse(outputter.HasCharactersPrinted());
            Assert.AreEqual(0, outputter.NumberOfCharactersPrinted());
        }

        [TestMethod]
        public void HasCharactersPrinted_PrintEmptyNewLine_IsFalse()
        {
            outputter.PrintLine("");
            Assert.IsFalse(outputter.HasCharactersPrinted());
            Assert.AreEqual(0, outputter.NumberOfCharactersPrinted());
        }

        [TestMethod]
        public void HasCharactersPrinted_PrintSpacesAndNewLines_IsFalse()
        {
            outputter.Print("   ");
            outputter.PrintLine("");
            outputter.Print("   ");
            outputter.PrintLine("");
            Assert.IsFalse(outputter.HasCharactersPrinted());
            Assert.AreEqual(0, outputter.NumberOfCharactersPrinted());
        }

        [TestMethod]
        public void NumberOfCharactersPrinted_TwoLines_IsTrue()
        {
            outputter.PrintLine("line 1");
            outputter.PrintLine("line 2");
            Assert.IsTrue(outputter.HasCharactersPrinted());
            Assert.AreEqual(12, outputter.NumberOfCharactersPrinted());
        }

        [TestMethod]
        public void ResetStatistics_ContainingCharacters_NoCharactersPrintedReported()
        {
            outputter.PrintLine("a text");
            Assert.IsTrue(outputter.HasCharactersPrinted());
            outputter.ResetStatistics();
            Assert.IsFalse(outputter.HasCharactersPrinted());
            Assert.AreEqual(0, outputter.NumberOfCharactersPrinted());
        }

    }
}
