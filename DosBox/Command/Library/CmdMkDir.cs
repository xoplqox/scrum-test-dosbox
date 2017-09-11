// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdMkDir : DosCommand
    {
        private static readonly string PARAMETER_CONTAINS_BACKLASH = "At least one parameter denotes a path rather than a directory name.";

        public CmdMkDir(string name, IDrive drive)
            : base(name, drive)
        {
        }

        protected override bool CheckNumberOfParameters(int number)
        {
            // Commands like "mkdir dir1 dir2 dir3" are allowed too.
            return number >= 1 ? true : false;
        }

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            for(int i=0 ; i<GetParameterCount() ; i++)
            {
                if (ParameterContainsBacklashes(GetParameterAt(i), outputter))
                    return false;
            }

            return true;
        }

        private static bool ParameterContainsBacklashes(string parameter, IOutputter outputter)
        {
            // Do not allow "mkdir c:\temp\dir1" to keep the command simple
            if (parameter.Contains("\\") || parameter.Contains("/"))
            {
                outputter.PrintLine(PARAMETER_CONTAINS_BACKLASH);
                return true;
            }
            return false;
        }

        public override void Execute(IOutputter outputter)
        {
            for(int i=0 ; i<GetParameterCount() ; i++)
            {
                CreateDirectory(GetParameterAt(i), this.Drive);
            }
        }

        private static void CreateDirectory(string newDirectoryName, IDrive drive)
        {
            Directory newDirectory = new Directory(newDirectoryName);
            drive.CurrentDirectory.Add(newDirectory);
        }
    }
}