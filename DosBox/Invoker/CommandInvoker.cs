// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBox.Invoker
{
    /// <summary>
    /// Invokes commands from a command string passed.
    /// Command-Pattern: Invoker
    /// 
    /// Responsibilities:
    /// - asks the command to carry out the request.
    ///   This is performed by executeCommand.
    /// - this class in independent of any concrete output channel like console, terminal, ...
    ///   This is ensured by the outputter interface.
    /// </summary>
    public class CommandInvoker : IExecuteCommand
    {
        private IList<DosCommand> commands;

        public CommandInvoker()
        {
            commands = new List<DosCommand>();
        }

        #region IExecuteCommand Members

        /// <summary>
        /// Interprets a command string, executes if an appropriate command is found
        /// and returns all output via the outputter interface.
        /// </summary>
        /// <param name="command">string which is entered, containing the command and the parameters.</param>
        /// <param name="outputter">Implementation of the outputter interface to which the output text is sent.</param>
        public void ExecuteCommand(string command, IOutputter outputter)
        {
            string cmdName = ParseCommandName(command);
            List<string> parameters = ParseCommandParameters(command);

            try
            {
                foreach (DosCommand cmd in commands)
                {
                    if (cmd.CompareCommandName(cmdName))
                    {
                        cmd.SetParameters(parameters);
                        if (cmd.CheckParameters(outputter))
                            cmd.Execute(outputter);
                        return;
                    }
                }

                outputter.PrintLine("\'" + cmdName + "\' is not recognized as an internal or external command,");
                outputter.PrintLine("operable program or batch file.");
            }
            catch (Exception e)
            {
                outputter.PrintLine(e.Message);
            }
        }

        #endregion

        /// <summary>
        /// Sets the list of active commands.
        /// Important: The current list is overwritten. This operation is intended to be used
        /// by the CommandFactory only at startup to set the reference to the
        /// corrent list. Use addCommand() for further expansions.
        /// </summary>
        /// <param name="newSetOfCommands">Reference to the list of active commands created by the factory.</param>
        public void SetCommands(IEnumerable<DosCommand> newSetOfCommands)
        {
            commands = newSetOfCommands.ToList();
        }

        /// <summary>
        /// Adds a new command to the list of active commands.
        /// </summary>
        /// <param name="cmd">New command which should be available by now.</param>
        public void AddCommand(DosCommand cmd)
        {
            commands.Add(cmd);
        }

        /// <summary>
        /// Extracts the command name from a given command string.
        /// The first word, before any space or comma is the command name
        /// Example:
        /// dir /w c:\temp
        /// Command name: dir
        /// </summary>
        /// <param name="command">The command string</param>
        /// <returns>Name of the command</returns>
        public string ParseCommandName(string command)
        {
            string cmd = command.ToLower();

            cmd = cmd.Trim();
            cmd = cmd.Replace(',', ' ');
            cmd = cmd.Replace(';', ' ');

            string cmdName = cmd;
            for (int i = 0; i < cmd.Length; i++)
            {
                if (cmd[i] == ' ')
                {
                    cmdName = cmd.Substring(0, i);
                    break;
                }
            }

            return cmdName;
        }

        /// <summary>
        /// Extracts the parameters from a given command string and returns a list of parameters.
        /// Parameters are separated by spaces, commas, semicolons.
        /// Example:
        /// dir /w c:\temp
        /// Parameter 1: /w
        /// Parameter 2: c:\temp
        /// </summary>
        /// <param name="command">The command string</param>
        /// <returns>List of parameters</returns>
        public List<string> ParseCommandParameters(string command)
        {
            var parameters = new List<string>();

            string cmd = command;
            cmd = cmd.Trim();
            cmd = cmd.Replace(',', ' ');
            cmd = cmd.Replace(';', ' ');

            int lastSpace = 0;
            for (int i = 0; i < cmd.Length; i++)
            {
                if (cmd[i] == ' ' || i + 1 == cmd.Length)
                {
                    parameters.Add(cmd.Substring(lastSpace, i + 1 - lastSpace).Trim());
                    lastSpace = i;
                }
            }

            var arguements = new List<string>();

            if (parameters.Count > 0)
            {
                // Remove command name
                parameters.RemoveAt(0);

                // Remove empty arguments
                arguements.AddRange(parameters.Where(param => param.CompareTo(string.Empty) != 0));
            }

            return arguements;
        }
    }
}