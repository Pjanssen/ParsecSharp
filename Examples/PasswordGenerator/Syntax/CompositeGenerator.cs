using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   class CompositeGenerator : IGenerator
   {
      private IEnumerable<IGenerator> generators;

      public CompositeGenerator(IEnumerable<IGenerator> generators)
      {
         this.generators = generators;
      }

      public string Generate(Random rand)
      {
         StringBuilder result = new StringBuilder();
         foreach (IGenerator generator in this.generators)
         {
            result.Append(generator.Generate(rand));
         }
         return result.ToString();
      }
   }
}
