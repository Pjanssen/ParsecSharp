using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   class CharLiteralGenerator : IGenerator
   {
      private string character;
      public CharLiteralGenerator(char c)
      {
         this.character = c.ToString();
      }

      public string Generate(Random rand)
      {
         return this.character;
      }
   }
}
