using PJanssen.ParsecSharp.IO;
using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public abstract class Parser<T>
   {
      public abstract Either<T, ParseError> Parse(IInputReader input);

      /// <summary>
      /// Labels the parser with a message that is added to a potential Error value.
      /// </summary>
      public Parser<T> Label(Func<string> msgFunc)
      {
         return new LabeledParser<T>(this, msgFunc);
      }

      /// <summary>
      /// Applies a projection function to the result of a parser.
      /// </summary>
      public Parser<TResult> Select<TResult>(Func<T, TResult> func)
      {
         return new SelectParser<T, TResult>(this, func);
      }

      /// <summary>
      /// Combines the results of two Parsers.
      /// </summary>
      public Parser<TResult> SelectMany<TSourceB, TResult>(Func<T, Parser<TSourceB>> func,
                                                           Func<T, TSourceB, TResult> combine)
      {
         return new SelectManyParser<T, TSourceB, TResult>(this, func, combine);
      }

      /// <summary>
      /// Tests the given predicate for this parser and returns an error if it fails.
      /// </summary>
      public Parser<T> Where(Predicate<T> predicate)
      {
         return new WhereParser<T>(this, predicate);
      }
   }
}
