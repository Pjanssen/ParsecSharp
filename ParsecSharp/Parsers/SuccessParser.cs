using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class SuccessParser<T> : IParser<T>
   {
      private T value;

      public SuccessParser(T value)
      {
         this.value = value;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         return ParseResult.Success(this.value);
      }
   }
}
