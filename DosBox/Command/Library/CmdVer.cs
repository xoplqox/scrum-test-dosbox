using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DosBox.Command.Framework;
using DosBox.Interfaces;

namespace DosBox.Command.Library
{
  public class CmdVer : DosCommand
  {
    public CmdVer(string name, IDrive drive)
            : base(name, drive)
    {

    }


    public override bool Execute(IOutputter outputter)
    {
      if( GetParameterCount() > 0 )
      {
        if (GetParameterCount() == 1 && GetParameterAt(0).Equals("/w", StringComparison.InvariantCultureIgnoreCase))
        {
          outputter.PrintLine("Microsoft Windows [Version 6.1.7601]");
          outputter.PrintLine("Hans");
          outputter.PrintLine("Ahmend");
          outputter.PrintLine("Caglar");
          outputter.PrintLine("Frowin");
          outputter.PrintLine("Axel");
          return true;
        }
        else
        {
          outputter.PrintLine("invalid parameter");
          return false;
        }
      }
      else
      {
        outputter.PrintLine("Microsoft Windows [Version 6.1.7601]");
        return true;
      }

      return false;
    }
  }
}
