using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   internal interface IQuantifier
   {
      int NumRepeats(Random rand);
   }
}
