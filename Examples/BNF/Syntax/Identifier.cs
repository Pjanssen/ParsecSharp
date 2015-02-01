using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNF.Syntax
{
   public class Identifier : NonTerminal
   {
      public Identifier(string name)
      {
         Name = name;
      }

      public string Name
      {
         get;
         private set;
      }

      public override string ToString()
      {
         return "<" + Name + ">";
      }
   }
}
