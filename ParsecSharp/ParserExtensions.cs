using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class ParserExtensions
   {
      public static Either<T, ParseError> Parse<T>(this Parser<T> parser, string input)
      {
         IInputReader reader = InputReader.Create(input);
         return parser.Parse(reader);
      }

      public static Either<T, ParseError> Parse<T>(this Parser<T> parser, Stream input, Encoding encoding)
      {
         IInputReader reader = InputReader.Create(input, encoding);
         return parser.Parse(reader);
      }
   }
}
