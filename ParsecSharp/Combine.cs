using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Combine
   {
      /// <summary>
      /// Applies the first parser and returns its value if it succeeds. 
      /// If it fails without consuming any input, the second parser is applied.
      /// </summary>
      public static Parser<TValue> Or<TValue>(Parser<TValue> parserA, Parser<TValue> parserB)
      {
         return input =>
         {
            int position = input.Position;

            var result = parserA(input);
            if (result.IsSuccess() || input.Position != position)
               return result;

            return parserB(input);
         };
      }

      /// <summary>
      /// Tries to apply the given parsers in order, until one of them succeeds. 
      /// Returns the value of the succeeding parser.
      /// </summary>
      public static Parser<TValue> Choose<TValue>(params Parser<TValue>[] parsers)
      {
         return Choose((IEnumerable<Parser<TValue>>)parsers);
      }

      /// <summary>
      /// Tries to apply the given parsers in order, until one of them succeeds. 
      /// Returns the value of the succeeding parser.
      /// </summary>
      public static Parser<TValue> Choose<TValue>(IEnumerable<Parser<TValue>> parsers)
      {
         return parsers.Aggregate( Parse.Error<TValue>("Empty choose sequence")
                                 , (acc, p) => Combine.Or(acc, p));
      }
   }
}
