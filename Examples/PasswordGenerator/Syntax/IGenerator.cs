using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   public interface IGenerator
   {
      string Generate(Random rand);
   }
}
