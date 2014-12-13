using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class WhereParser<T> : Parser<T>
   {
      private Parser<T> parser;
      private Predicate<T> predicate;

      public WhereParser(Parser<T> parser, Predicate<T> predicate)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(predicate, "predicate");

         this.parser = parser;
         this.predicate = predicate;
      }

      public override Either<T, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsError())
            return result;

         T resultValue = result.FromSuccess();
         if (this.predicate(resultValue))
            return result;

         return ParseResult.UnexpectedValue(input, resultValue);
      }
   }
}
