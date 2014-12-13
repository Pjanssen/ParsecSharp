using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class SelectParser<TSource, TResult> : Parser<TResult>
   {
      private Parser<TSource> parser;
      private Func<TSource, TResult> func;

      public SelectParser(Parser<TSource> parser, Func<TSource, TResult> func)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(func, "func");

         this.parser = parser;
         this.func = func;
      }

      public override Either<TResult, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsError())
            return ParseResult.Error<TResult>(result.FromError());

         return ParseResult.Success(this.func(result.FromSuccess()));
      }
   }
}
