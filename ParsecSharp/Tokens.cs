using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Defines parsers for lexical elements.
   /// </summary>
   public static class Tokens
   {
      /// <summary>
      /// Applies the given parser and consumes any following whitespace. Returns the parsed value.
      /// </summary>
      public static Parser<T> Lexeme<T>(Parser<T> parser)
      {
         return from value in parser
                from _ in Chars.WhiteSpace().Many()
                select value;
      }

      /// <summary>
      /// Parses the given symbol name followed by any whitespace. Returns the parsed symbol name.
      /// </summary>
      public static Parser<string> Symbol(string name)
      {
         return Lexeme<string>(Chars.String(name));
      }
   }
}
