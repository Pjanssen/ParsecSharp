using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNF.Syntax
{
   public class Terminal : NonTerminal
   {
      public Terminal(string value)
      {
         Value = value;
      }

      public string Value
      {
         get;
         private set;
      }

      public override string ToString()
      {
         return "\"" + Value + "\"";
      }
   }
}
