using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   class RangeQuantifier : IQuantifier
   {
      private int min;
      private int max;

      public RangeQuantifier(int min, int max)
      {
         this.min = min;
         this.max = max;
      }

      public int NumRepeats(Random rand)
      {
         return rand.Next(this.min, this.max + 1);
      }
   }
}
