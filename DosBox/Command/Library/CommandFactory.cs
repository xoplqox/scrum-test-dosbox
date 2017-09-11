// DOSBox, Scrum.org, Professional Scrum Developer Training
// Authors: Rainer Grau, Daniel Tobler, Zühlke Technology Group
// Copyright (c) 2012 All Right Reserved

using System.Collections.Generic;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    /// <summary>
    /// The factory is responsible to create an object of every command supported
    /// and to add it to the list of known commands.
    /// New commands must be added to the list of known commands here.
    /// </summary>
    public class CommandFactory
    {
        private readonly List<DosCommand> commands;

        public CommandFactory(IDrive drive)
        {
            commands = new List<DosCommand>
                           {
                               new CmdDir("dir", drive),
                               new CmdCd("cd", drive),
                               new CmdCd("chdir", drive),
                               new CmdMkDir("mkdir", drive),
                               new CmdMkDir("md", drive),
                               new CmdMkFile("mf", drive),
                               new CmdMkFile("mkfile", drive)

                               // Add your commands here

                           };
        }

        public IEnumerable<DosCommand> CommandList
        {
            get { return commands; }
        }
    }
}