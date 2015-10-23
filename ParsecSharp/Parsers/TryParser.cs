using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class TryParser<T> : IParser<T>
   {
      private IParser<T> parser;

      public TryParser(IParser<T> parser)
      {
         Throw.IfNull(parser, "parser");

         this.parser = parser;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         Position position = input.GetPosition();

         var result = this.parser.Parse(input);
         if (result.IsError)
            input.Seek(position);

         return result;
      }
   }
}
