using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class AnyCharParser : IParser<char>
   {
      public IEither<char, ParseError> Parse(IInputReader input)
      {
         if (input.EndOfStream)
            return ParseResult.Error<char>(input, "Unexpected end of input");

         char c = input.Read();
         return ParseResult.Success(c);
      }
   }
}
