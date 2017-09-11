// DOSBox, Scrum.org, PSD.Java training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using DosBox.Filesystem;
using DosBox.Interfaces;
using DosBox.Invoker;
using DosBoxTest.Command.Framework;
using DosBoxTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DosBoxTest.Invoker
{
    [TestClass]
    public class CommandInvokerTest
    {
        private CommandInvoker commandInvoker;
        private TestOutput output;
        private TestCommand testcmd;

        [TestInitialize]
        public void setUp()
        {
            IDrive drive = new Drive("C");
            commandInvoker = new CommandInvoker();
            testcmd = new TestCommand("dIR", drive);
            commandInvoker.AddCommand(testcmd);

            output = new TestOutput();
        }

        [TestMethod]
        public void ParseCommandName_EmptyString()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName(""), "");
        }

        [TestMethod]
        public void ParseCommandName_OnlyCommandName()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName("dir"), "dir");
        }

        [TestMethod]
        public void ParseCommandName_UpperCaseCommandName_ConvertsToLowerCase()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName("DIR"), "dir");
        }

        [TestMethod]
        public void ParseCommandName_OneParameter_ExtractsCommandName()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName("dir param1"), "dir");
        }

        [TestMethod]
        public void ParseCommandName_SeparationByCommand_ExtractsCommandName()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName("dir,param1, param2"), "dir");
        }

        [TestMethod]
        public void ParseCommandName_EndsWithComma_ExtractsCommandName()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName("dir,"), "dir");
        }

        [TestMethod]
        public void ParseCommandName_EndingWhiteSpaces_ExtractsCommandName()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName("dir   "), "dir");
        }

        [TestMethod]
        public void ParseCommandName_SingleLetterParameter_ExtractsCommandName()
        {
            Assert.AreEqual(commandInvoker.ParseCommandName("dir o"), "dir");
        }

        [TestMethod]
        public void ParseCommandParameters_NoParameters_EmptyList()
        {
            List<string> parameters = commandInvoker.ParseCommandParameters("dir");
            Assert.IsTrue(parameters.Count == 0);
        }

        [TestMethod]
        public void testParseParameters_OneParameter_OneEntryInList()
        {
            List<string> parameters = commandInvoker.ParseCommandParameters("dir /p");
            Assert.IsTrue(parameters.Count == 1);
            Assert.IsTrue(parameters[0].CompareTo("/p") == 0);
        }

        [TestMethod]
        public void testParseParameters_TwoParameters_TwoEntriesInList()
        {
            List<string> parameters = commandInvoker.ParseCommandParameters("dir /p param2");
            Assert.AreEqual(2, parameters.Count);
            Assert.AreEqual("/p", parameters[0]);
            Assert.AreEqual("param2", parameters[1]);
        }

        [TestMethod]
        public void testParseParameters_OneParameterWithSeveralSpaces_OneEntryInList()
        {
            List<string> parameters = commandInvoker.ParseCommandParameters("dir    /p");
            Assert.AreEqual(1, parameters.Count);
            Assert.AreEqual("/p", parameters[0]);
        }

        [TestMethod]
        public void testParseParameters_TwoParametersWithSeveralSpaces_TwoEntriesInList()
        {
            List<string> parameters = commandInvoker.ParseCommandParameters("dir  param1  param2   ");
            Assert.AreEqual(2, parameters.Count);
            Assert.AreEqual("param1", parameters[0]);
            Assert.AreEqual("param2", parameters[1]);
        }

        [TestMethod]
        public void testParseParameters_TwoParametersSingleCharacters_TwoEntriesInList()
        {
            List<string> parameters = commandInvoker.ParseCommandParameters("d 1 2");
            Assert.AreEqual(2, parameters.Count);
            Assert.AreEqual("1", parameters[0]);
            Assert.AreEqual("2", parameters[1]);
        }

        [TestMethod]
        public void testCommandExecuteSimple()
        {
            commandInvoker.ExecuteCommand("DIR", output);
            Assert.IsTrue(testcmd.Executed);
        }

        [TestMethod]
        public void testCommandExecuteWithLeadingSpace()
        {
            commandInvoker.ExecuteCommand("   DIR", output);
            Assert.IsTrue(testcmd.Executed);
        }

        [TestMethod]
        public void testCommandExecuteWithEndingSpace()
        {
            commandInvoker.ExecuteCommand("DIR   ", output);

            Assert.IsTrue(testcmd.Executed);
        }

        [TestMethod]
        public void ExecuteCommand_CommandOnly_IsExecuted()
        {
            commandInvoker.ExecuteCommand("dir", output);
            Assert.IsTrue(testcmd.Executed);
        }

        [TestMethod]
        public void ExecuteCommand_CommandWithParameters_IsExecutedParametersPassed()
        {
            commandInvoker.ExecuteCommand("dir param1 param2", output);
            Assert.IsTrue(testcmd.Executed);
            Assert.AreEqual(2, testcmd.getParameters().Count);
            Assert.AreEqual(2, testcmd.NumberOfPassedParameters);
        }

        [TestMethod]
        public void ExecuteCommand_CommandWithInvalidSyntax_NotExecutedErrorPrinted()
        {
            testcmd.CheckNumberOfParametersReturnValue = false;
            commandInvoker.ExecuteCommand("dir param1 param2", output);
            Assert.IsFalse(testcmd.Executed);
            Assert.AreEqual(2, testcmd.getParameters().Count);
            Assert.AreEqual(2, testcmd.NumberOfPassedParameters);
            TestHelper.AssertContainsNoCase("syntax of the command is incorrect", output);
        }

        [TestMethod]
        public void ExecuteCommand_CommandWithWrongNumberOfParameters_NotExecutedErrorPrinted()
        {
            testcmd.CheckParameterValuesReturnValue = false;
            commandInvoker.ExecuteCommand("dir param1 param2", output);
            Assert.IsFalse(testcmd.Executed);
            Assert.AreEqual(2, testcmd.getParameters().Count);
            Assert.AreEqual(2, testcmd.NumberOfPassedParameters);
            TestHelper.AssertContainsNoCase("wrong", output);
            TestHelper.AssertContainsNoCase("parameter", output);
        }
    }
}