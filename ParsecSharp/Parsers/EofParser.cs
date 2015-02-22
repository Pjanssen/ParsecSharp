using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class EofParser<T> : IParser<T>
   {
      public IEither<T, ParseError> Parse(IInputReader input)
      {
         if (input.EndOfStream)
            return ParseResult.Success(default(T));

         return ParseResult.Error<T>(input, "Expected end of input");
      }
   }
}
