using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
    class CmdExit : Framework.DosCommand
    {
        public CmdExit(string commandName, IDrive drive)
            : base(commandName, drive)
        {
        }

        public override bool Execute(IOutputter outputter)
        {
          return true;// throw new NotImplementedException();
        }
    }
}
