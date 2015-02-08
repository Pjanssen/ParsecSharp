using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class SelectManyParser<TSourceA, TSourceB, TResult> : Parser<TResult>
   {
      private Parser<TSourceA> parser;
      private Func<TSourceA, Parser<TSourceB>> func;
      private Func<TSourceA, TSourceB, TResult> combine;

      public SelectManyParser(Parser<TSourceA> parser, Func<TSourceA, Parser<TSourceB>> func, Func<TSourceA, TSourceB, TResult> combine)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(func, "func");
         Throw.IfNull(combine, "combine");

         this.parser = parser;
         this.func = func;
         this.combine = combine;
      }

      public override Either<TResult, ParseError> Parse(IInputReader input)
      {
         return from resultA in parser.Parse(input)
                from resultB in func(resultA).Parse(input)
                select combine(resultA, resultB);
      }
   }
}
