// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using DosBox.Command.Framework;
using DosBox.Filesystem;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    public class CmdMkFile : DosCommand
    {
        public CmdMkFile(string cmdName, IDrive drive)
            : base(cmdName, drive)
        {
        }

        public override void Execute(IOutputter outputter)
        {
            string fileName = GetParameterAt(0);
            string fileContent = GetParameterAt(1);
            File newFile = new File(fileName, fileContent);
            this.Drive.CurrentDirectory.Add(newFile);
        }
    }
}