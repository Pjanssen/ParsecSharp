using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   class CharSetGenerator : IGenerator
   {
      private string characters;

      public CharSetGenerator(string characters)
      {
         this.characters = characters;
      }

      public string Generate(Random rand)
      {
         int next = rand.Next(this.characters.Length);
         return characters[next].ToString();
      }
   }
}
