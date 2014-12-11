using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator.Syntax
{
   class QuantifiedExpression : IGenerator
   {
      private IGenerator expression;
      private IQuantifier quantifier;

      public QuantifiedExpression(IGenerator expression, IQuantifier quantifier)
      {
         this.expression = expression;
         this.quantifier = quantifier;
      }

      public string Generate(Random rand)
      {
         int numRepeats = this.quantifier.NumRepeats(rand);
         StringBuilder result = new StringBuilder();

         for (int i = 0; i < numRepeats; i++)
         {
            result.Append(this.expression.Generate(rand));
         }

         return result.ToString();
      }
   }
}
