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
      private static readonly IParser<string> WhiteSpace = Chars.WhiteSpace().Many();

      /// <summary>
      /// Applies the given parser and consumes any following whitespace. Returns the parsed value.
      /// </summary>
      public static IParser<T> Lexeme<T>(IParser<T> parser)
      {
         return from value in parser
                from _ in WhiteSpace
                select value;
      }

      /// <summary>
      /// Parses the given symbol followed by any whitespace. Returns the parsed symbol.
      /// </summary>
      public static IParser<string> Symbol(string symbol)
      {
         return Lexeme(Chars.String(symbol));
      }

      /// <summary>
      /// Parses the given symbol followed by any whitespace. Returns the parsed symbol.
      /// </summary>
      public static IParser<char> Symbol(char symbol)
      {
         return Lexeme(Chars.Char(symbol));
      }
   }
}
