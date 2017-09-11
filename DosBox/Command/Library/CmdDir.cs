// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdDir : DosCommand
    {
        private readonly string SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED = "File Not Found"; 
        private Directory directoryToPrint;

        public CmdDir(string name, IDrive drive)
            : base(name, drive)
        {
        }

        protected override bool CheckNumberOfParameters(int numberOfParameters)
        {
            return numberOfParameters == 0 || numberOfParameters == 1;
        }

        protected override bool CheckParameterValues(IOutputter outputter)
        {
            if (GetParameterCount() == 0)
            {
                directoryToPrint = Drive.CurrentDirectory;
            }
            else
            {
                this.directoryToPrint = CheckAndPreparePathParameter(GetParameterAt(0), outputter);
            }
            return this.directoryToPrint != null;
        }

        private Directory CheckAndPreparePathParameter(string pathName, IOutputter outputter)
        {
            FileSystemItem fsi = Drive.GetItemFromPath(pathName);
            if (fsi == null)
            {
                outputter.PrintLine(SYSTEM_CANNOT_FIND_THE_PATH_SPECIFIED);
                return null;
            }
            if (!fsi.IsDirectory())
            {
                return fsi.Parent;
            }
            return (Directory)fsi;
        }

        public override void Execute(IOutputter outputter)
        {
            PrintHeader(this.directoryToPrint, outputter);
            PrintContent(this.directoryToPrint.Content, outputter);
            PrintFooter(this.directoryToPrint, outputter);
        }

        private static void PrintHeader(Directory directoryToPrint, IOutputter outputter)
        {
            outputter.PrintLine("Directory of " + directoryToPrint.Path);
            outputter.NewLine();
        }

        private static void PrintContent(IEnumerable<FileSystemItem> directoryContent, IOutputter outputter)
        {
            foreach (FileSystemItem item in directoryContent)
            {
                if (item.IsDirectory())
                {
                    outputter.Print("<DIR>");
                }
                else
                {
                    outputter.Print("" + item.GetSize());
                }

                outputter.Print("\t" + item.Name);
                outputter.NewLine();
            }
        }

        private static void PrintFooter(Directory directoryToPrint, IOutputter outputter)
        {
            outputter.PrintLine("\t" + directoryToPrint.GetNumberOfContainedFiles() + " File(s)");
            outputter.PrintLine("\t" + directoryToPrint.GetNumberOfContainedDirectories() + " Dir(s)");
        }
    }
}