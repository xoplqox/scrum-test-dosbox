// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Library;
using DosBox.Console;
using DosBox.Filesystem;
using DosBox.Interfaces;
using DosBox.Invoker;

namespace DosBox.Configuration
{
    /// <summary>
    /// Configures the system.
    /// </summary>
    public class Configurator
    {
        /// <summary>
        /// Configurates the system for a console application.
        /// </summary>
        public void ConfigurateSystem()
        {
            // Create file system with initial root directory
            // and read any persistent information.
            IDrive drive = new Drive("C");
            drive.Restore();

            // Create all commands and invoker
            CommandFactory factory = new CommandFactory(drive);
            CommandInvoker commandInvoker = new CommandInvoker();
            commandInvoker.SetCommands(factory.CommandList);
            IExecuteCommand invoker = commandInvoker;

            // Setup console for input and output
            ConsoleEx console = new ConsoleEx(invoker, drive);

            // Start console
            console.ProcessInput();
        }
    }
}