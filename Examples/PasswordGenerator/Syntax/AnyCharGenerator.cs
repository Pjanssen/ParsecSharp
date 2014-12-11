using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   internal class AnyCharGenerator : IGenerator
   {
      public string Generate(Random rand)
      {
         int next = (int)'0' + rand.Next((int)'Z');
         return ((char)next).ToString();
      }
   }
}
