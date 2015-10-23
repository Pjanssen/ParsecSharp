using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class AggregateParser<T, TAccum, TResult> : IParser<TResult>
   {
      private IParser<T> parser;
      private Func<TAccum> seed;
      private Func<TAccum, T, TAccum> func;
      private Func<TAccum, TResult> resultSelector;

      public AggregateParser(IParser<T> parser, Func<TAccum> seed, Func<TAccum, T, TAccum> func, Func<TAccum, TResult> resultSelector)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(seed, "seed");
         Throw.IfNull(func, "func");
         Throw.IfNull(resultSelector, "resultSelector");

         this.parser = parser;
         this.seed = seed;
         this.func = func;
         this.resultSelector = resultSelector;
      }

      public IEither<TResult, ParseError> Parse(IInputReader input)
      {
         TAccum acc = this.seed();

         IEither<T, ParseError> result = null;
         Position position = input.GetPosition();

         while ((result = this.parser.Parse(input)).IsSuccess)
         {
            acc = this.func(acc, result.FromSuccess());
            position = input.GetPosition();
         }

         if (input.GetPosition() == position)
            return ParseResult.Success(this.resultSelector(acc));
         else
            return ParseResult.Error<TResult>(result.FromError());
      }
   }
}
