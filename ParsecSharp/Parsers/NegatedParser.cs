using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class NegatedParser<T> : Parser<Unit>
   {
      private Parser<T> parser;

      public NegatedParser(Parser<T> parser)
      {
         Throw.IfNull(parser, "parser");

         this.parser = parser;
      }

      public override Either<Unit, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsSuccess())
            return ParseResult.UnexpectedValue<Unit>(input, result.FromSuccess());

         return ParseResult.Success(Unit.Instance);
      }
   }
}
