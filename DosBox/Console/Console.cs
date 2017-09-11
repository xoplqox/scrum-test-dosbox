// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System;
using System.IO;
using System.Text;
using DosBox.Interfaces;

namespace DosBox.Console
{
    /// <summary>
    /// Implements a console. The user is able to input command strings
    /// and receives the output directly on that console.
    /// Configures the Invoker, the Commands and the Filesystem.
    /// </summary>
    public class ConsoleEx
    {
        private readonly IDrive drive;
        private readonly IExecuteCommand invoker;
        private readonly IOutputter outputter;

        public ConsoleEx(IExecuteCommand invoker, IDrive drive)
        {
            this.invoker = invoker;
            this.drive = drive;
            outputter = new ConsoleOutputter();
        }

        /// <summary>
        /// Processes input from the console and invokes the invoker until 'exit' is typed.
        /// </summary>
        public void ProcessInput()
        {
            string line = string.Empty;
            outputter.PrintLine("DOSBox, Scrum.org, Professional Scrum Developer Training");
            outputter.PrintLine("Copyright (c) Rainer Grau and Daniel Tobler. All rights reserved.");

            while (line.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase) == false)
            {
                int readChar = 0;
                var input = new StringBuilder();

                outputter.NewLine();
                outputter.Print(drive.Prompt);
                try
                {
                    while (readChar != '\n')
                    {
                        readChar = System.Console.Read();
                        input.Append((char) readChar);
                    }

                    line = input.ToString();
                }
                catch (IOException)
                {
                    // do nothing by intention
                }

                outputter.ResetStatistics();
                invoker.ExecuteCommand(line, outputter);
            }

            outputter.PrintLine("\nGoodbye!");
            drive.Save();
        }
    }
}