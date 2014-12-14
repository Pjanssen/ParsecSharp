using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class PredicateParser<T> : Parser<T>
   {
      private Parser<T> parser;

      public PredicateParser(Parser<T> parser, Predicate<T> predicate)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(predicate, "predicate");

         this.parser = parser;
         this.Predicate = predicate;
      }

      public Predicate<T> Predicate
      {
         get;
         set;
      }

      public override Either<T, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsError())
            return result;

         T resultValue = result.FromSuccess();
         if (this.Predicate(resultValue))
            return result;

         return ParseResult.UnexpectedValue(input, resultValue);
      }
   }
}
