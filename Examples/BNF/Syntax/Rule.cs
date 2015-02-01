using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNF.Syntax
{
   public class Rule
   {
      public Rule(Identifier identfier, NonTerminal value)
      {
         this.Identifier = identfier;
         this.Value = value;
      }

      public Identifier Identifier
      {
         get;
         private set;
      }

      public NonTerminal Value
      {
         get;
         private set;
      }

      public override string ToString()
      {
         return Identifier.ToString() + " ::= " + Value.ToString();
      }
   }
}
