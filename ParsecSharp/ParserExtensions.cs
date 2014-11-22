using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class ParserExtensions
   {
      /// <summary>
      /// Runs the parser with the given input string.
      /// </summary>
      public static Either<TValue, string> Run<TValue>(this Parser<TValue> parser, string input)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(input, "input");

         TextReader reader = new StringReader(input);
         return parser(reader);
      }

      /// <summary>
      /// Runs the parser with the given input stream.
      /// </summary>
      public static Either<TValue, string> Run<TValue>(this Parser<TValue> parser, Stream input)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(input, "input");

         TextReader reader = new StreamReader(input);
         return parser(reader);
      }
   }
}
