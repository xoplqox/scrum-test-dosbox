// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    /// <summary>
    /// Command to change current directory.
    /// Example for a command with optional parameters.
    /// </summary>
    public class CmdCd : DosCommand
    {
        private static readonly string SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED = "The system cannot find the path specified.";
        private static readonly string DESTINATION_IS_FILE = "The directory name is invalid.";
        private Directory destinationDirectory;

        public CmdCd(string name, IDrive drive)
            : base(name, drive)
        {
        }

        protected override bool CheckNumberOfParameters(int numberOfParametersEntered)
        {
            return numberOfParametersEntered == 0 || numberOfParametersEntered == 1;
        }

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            if (GetParameterCount() > 0)
            {
                this.destinationDirectory = ExtractAndCheckIfValidDirectory(GetParameterAt(0), this.Drive, outputter);
                return this.destinationDirectory != null;
            }
            else
            {
                this.destinationDirectory = null;
                return true;
            }
        }

        public override void Execute(IOutputter outputter)
        {
            if (GetParameterCount() == 0)
            {
                PrintCurrentDirectoryPath(this.Drive.CurrentDirectory.Path, outputter);
            }
            else
            {
                ChangeCurrentDirectory(this.destinationDirectory, this.Drive, outputter);
            }
        }

        private static void ChangeCurrentDirectory(Directory destinationDirectory, IDrive drive, IOutputter outputter)
        {
            bool success = drive.ChangeCurrentDirectory(destinationDirectory);
            if (!success)
            {
                outputter.PrintLine(SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED);
            }
        }

        private static void PrintCurrentDirectoryPath(string currentDirectoryName, IOutputter outputter)
        {
            outputter.PrintLine(currentDirectoryName);
        }

        private static Directory ExtractAndCheckIfValidDirectory(string destinationDirectoryName, IDrive drive, IOutputter outputter)
        {
            FileSystemItem tempDestinationDirectory = drive.GetItemFromPath(destinationDirectoryName);
            if (tempDestinationDirectory == null)
            {
                outputter.PrintLine(SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED);
                return null;
            }
            if (!tempDestinationDirectory.IsDirectory())
            {
                outputter.PrintLine(DESTINATION_IS_FILE);
                return null;
            }
            return (Directory)tempDestinationDirectory;
        }
    }
}