using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class ErrorParser<T> : Parser<T>
   {
      private string message;
      public ErrorParser(string message)
      {
         this.message = message;
      }

      public override Either<T, ParseError> Parse(IInputReader input)
      {
         return ParseResult.Error<T>(input, this.message);
      }
   }
}
