using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Defines commonly used character parsers.
   /// </summary>
   public static class Chars
   {
      /// <summary>
      /// Succeeds for any character. Returns the parsed character.
      /// </summary>
      public static Parser<char> Any()
      {
         return new AnyCharParser();
      }

      /// <summary>
      /// Succeeds for any character for which the given predicate returns true. Returns the parsed character.
      /// </summary>
      public static Parser<char> Satisfy(Predicate<char> predicate)
      {
         Throw.IfNull(predicate, "predicate");

         return Parse.Try(from c in Any()
                          where predicate(c)
                          select c);
      }

   }
}
