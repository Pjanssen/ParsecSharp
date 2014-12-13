using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class TryParser<T> : Parser<T>
   {
      private Parser<T> parser;

      public TryParser(Parser<T> parser)
      {
         Throw.IfNull(parser, "parser");

         this.parser = parser;
      }

      public override Either<T, ParseError> Parse(IInputReader input)
      {
         Position position = input.GetPosition();

         var result = this.parser.Parse(input);
         if (result.IsError())
            input.Seek(position);

         return result;
      }
   }
}
