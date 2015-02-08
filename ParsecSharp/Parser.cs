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
      /// Applies the parser until it fails, combining the results using the given accumulator function and initial value.
      /// </summary>
      /// <typeparam name="TAccum">The type of the aggregated value</typeparam>
      /// <param name="seed">A function that creates the initial accumulator value</param>
      /// <param name="func">An accumulator function that takes the current accumulated value, the currently parsed result, and combines them into a new accumulated value.</param>
      public Parser<TAccum> Aggregate<TAccum>(Func<TAccum> seed, Func<TAccum, T, TAccum> func)
      {
         return Aggregate(seed, func, x => x);
      }

      /// <summary>
      /// Applies the parser until it fails, combining the results using the given accumulator function and initial value.
      /// </summary>
      /// <typeparam name="TAccum">The type of the aggregated value</typeparam>
      /// <param name="seed">A function that creates the initial accumulator value</param>
      /// <param name="func">An accumulator function that takes the current accumulated value, the currently parsed result, and combines them into a new accumulated value.</param>
      /// <param name="resultSelector">A function that creates the final aggregated result.</param>
      public Parser<TResult> Aggregate<TAccum, TResult>(Func<TAccum> seed,
                                                        Func<TAccum, T, TAccum> func,
                                                        Func<TAccum, TResult> resultSelector)
      {
         return new AggregateParser<T, TAccum, TResult>(this, seed, func, resultSelector);
      }

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
         return new PredicateParser<T>(this, predicate);
      }

      /// <summary>
      /// Succeeds only if the given parser fails. Returns the default value of T.
      /// </summary>
      public static Parser<T> operator !(Parser<T> parser)
      {
         return from n in ParsecSharp.Parse.Not<T>(parser)
                select default(T);
      }

      /// <summary>
      /// Applies the first parser and returns its value if it succeeds. 
      /// If it fails without consuming any input, the second parser is applied.
      /// </summary>
      public static Parser<T> operator |(Parser<T> parserA, Parser<T> parserB)
      {
         return Combine.Or(parserA, parserB);
      }

      /// <summary>
      /// Tries to apply the first parser and returns its value if it succeeds.
      /// If it fails, the second parser is applied. This is equivalent to "Parse.Try(parserA) | parserB".
      /// </summary>
      public static Parser<T> operator ^(Parser<T> parserA, Parser<T> parserB)
      {
         return ParsecSharp.Parse.Try(parserA) | parserB;
      }
   }
}
