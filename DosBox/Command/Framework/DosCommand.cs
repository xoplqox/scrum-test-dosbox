// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.Collections.Generic;
using DosBox.Interfaces;

namespace DosBox.Command.Framework
{
    /// <summary>
    /// Implements the abstract base class for commands.
    /// </summary>
    public abstract class DosCommand
    {
        private static readonly string INCORRECT_SYNTAX = "The syntax of the command is incorrect.";
        private static readonly string DEFAULT_ERROR_MESSAGE_WRONG_PARAMETER = "Wrong parameter entered";
        private readonly string commandName;
        public IDrive Drive { get; private set; }
        private IList<string> parameters = new List<string>();

        protected DosCommand(string commandName, IDrive drive)
        {
            this.commandName = commandName.ToLower();
            this.Drive = drive;
        }

        /// <summary>
        /// Checks the passed parameters. This consists of two steps which are overrideable by concrete commands.
        /// 1.) Check the number of parameters.
        /// 2.) Check the values of all passed parameters
        /// Template Method Pattern: Template Method.
        /// </summary>
        /// <param name="outputter">The outputter must be used to printout any error description.</param>
        /// <returns>
        /// - true  if number and values of the parameters are correct. Execute() may use the parameters afterwards unchecked.
        /// - false if the number is below or above excepted range or if any value is incorrect. An explaining error message must be given
        ///         by the concrete command. 
        /// </returns>
        public bool CheckParameters(IOutputter outputter)
        {
            if (!CheckNumberOfParameters(parameters.Count))
            {
                outputter.PrintLine(INCORRECT_SYNTAX);
                return false;
            }
            if (!CheckParameterValues(outputter))
            {
                if (!outputter.HasCharactersPrinted())
                    outputter.PrintLine(DEFAULT_ERROR_MESSAGE_WRONG_PARAMETER);
                return false;
            }
            return true;
        }

        /// <summary>
        /// *Can be overwritten* by the concrete commands if something must be checked.
        /// Checks if the number of parameters is in range.
        /// Do not output anything, an explaining error message is output by the abstract command.
        /// Template Method Pattern: Hook.
        /// </summary>
        /// <param name="number">Number of parameters passed by the caller.</param>
        /// <returns>
        /// <li/> true if number of parameters is within expected range
        /// <li/> false otherwise
        /// </returns>
        protected virtual bool CheckNumberOfParameters(int numberOfParameters)
        {
            return true;
        }

        /// <summary>
        /// *Can be overwritten* by the concrete commands if at least the value of one parameter must be checked.
        /// Checks all values of all passed parameters.
        /// An explaining error message must be output by the concrete command.
        /// Template Method Pattern: Hook.
        /// </summary>
        /// <param name="parameters">The list of parameteters. Use to iterate
        /// <code>
        /// foreach(string parameter in parameters)
        /// {
        ///    // do something with parameter...
        /// }
        /// </code>
        /// </param>
        /// <param name="outputter">The output must be used to output error messages.</param>
        /// <returns>
        /// <li/> true if all values of all parameters passed are correct.
        /// <li/> false if at least one value of one parameter in incorrect.
        /// </returns>
        protected virtual bool CheckParameterValues(IOutputter outputter)
        {
            return true;
        }

        /// <summary>
        /// *Must be overwritten* by the concrete commands to implement the execution of the command.
        /// </summary>
        /// <param name="outputter">Must be used to printout any text.</param>
        public abstract void Execute(IOutputter outputter);

        /// <summary>
        /// Returns true if the passed name and the command name fit.
        /// Used to obtain the concrete command from the list of commands.
        /// </summary>
        /// <param name="commandNameToCompare">name with which the command name shall be compared.</param>
        /// <returns>
        /// <li/> true if names fit
        /// <li/> false otherwise
        /// </returns>
        public bool CompareCommandName(string commandNameToCompare)
        {
            return this.commandName.CompareTo(commandNameToCompare) == 0;
        }

        public void SetParameters(IEnumerable<string> newParameters)
        {
            this.parameters.Clear();
            foreach(string parameter in newParameters)
            {
                this.parameters.Add(parameter);
            }
        }

        protected int GetParameterCount()
        {
            return this.parameters.Count;
        }

        protected string GetParameterAt(int parameterIndex)
        {
            return this.parameters[parameterIndex];
        }

        public override string ToString()
        {
            return this.commandName;
        }
    }
}