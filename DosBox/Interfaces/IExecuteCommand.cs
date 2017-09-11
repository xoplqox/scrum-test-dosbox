// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;

namespace DosBox.Interfaces
{
    public interface IExecuteCommand
    {
        /// Interprets a command string, executes if an appropriate command is found
        /// and returns all output via the outputter interface.
        /// <param name="command">String which is entered, containing the command and the parameters.</param>
        /// <param name="outputter">Implementation of the outputter interface to which the output text is sent.</param>
        void ExecuteCommand(String command, IOutputter outputter);
    }
}