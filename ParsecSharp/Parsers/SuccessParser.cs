using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class SuccessParser<T> : Parser<T>
   {
      private T value;

      public SuccessParser(T value)
      {
         this.value = value;
      }

      public override Either<T, ParseError> Parse(IInputReader input)
      {
         return ParseResult.Success(this.value);
      }
   }
}
