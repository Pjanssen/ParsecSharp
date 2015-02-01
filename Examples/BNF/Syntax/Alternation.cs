using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNF.Syntax
{
   public class Alternation : NonTerminal
   {
      public Alternation(NonTerminal left, NonTerminal right)
      {
         Left = left;
         Right = right;
      }

      public NonTerminal Left
      {
         get;
         private set;
      }

      public NonTerminal Right
      {
         get;
         private set;
      }

      public override string ToString()
      {
         return Left.ToString() + " | " + Right.ToString();
      }
   }
}
