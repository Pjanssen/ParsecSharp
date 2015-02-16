using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class ErrorParser<T> : IParser<T>
   {
      private string message;
      public ErrorParser(string message)
      {
         this.message = message;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         return ParseResult.Error<T>(input, this.message);
      }
   }
}
