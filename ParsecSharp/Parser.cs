using PJanssen.ParsecSharp.IO;
using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Parser
   {
      /// <summary>
      /// Applies the parser until it fails, combining the results using the given accumulator function and initial value.
      /// </summary>
      /// <typeparam name="TAccum">The type of the aggregated value</typeparam>
      /// <param name="seed">A function that creates the initial accumulator value</param>
      /// <param name="func">An accumulator function that takes the current accumulated value, the currently parsed result, and combines them into a new accumulated value.</param>
      public static IParser<TAccum> Aggregate<T, TAccum>(this IParser<T> parser, Func<TAccum> seed, Func<TAccum, T, TAccum> func)
      {
         return Aggregate(parser, seed, func, x => x);
      }

      /// <summary>
      /// Applies the parser until it fails, combining the results using the given accumulator function and initial value.
      /// </summary>
      /// <typeparam name="TAccum">The type of the aggregated value</typeparam>
      /// <param name="seed">A function that creates the initial accumulator value</param>
      /// <param name="func">An accumulator function that takes the current accumulated value, the currently parsed result, and combines them into a new accumulated value.</param>
      /// <param name="resultSelector">A function that creates the final aggregated result.</param>
      public static IParser<TResult> Aggregate<T, TAccum, TResult>(this IParser<T> parser,
                                                                   Func<TAccum> seed,
                                                                   Func<TAccum, T, TAccum> func,
                                                                   Func<TAccum, TResult> resultSelector)
      {
         return new AggregateParser<T, TAccum, TResult>(parser, seed, func, resultSelector);
      }

      /// <summary>
      /// Labels the parser with a message that is added to a potential Error value.
      /// </summary>
      public static IParser<T> Label<T>(this IParser<T> parser, Func<string> msgFunc)
      {
         return new LabeledParser<T>(parser, msgFunc);
      }

      /// <summary>
      /// Runs the parser on the given string.
      /// </summary>
      public static IEither<T, ParseError> Parse<T>(this IParser<T> parser, string input)
      {
         IInputReader reader = InputReader.Create(input);
         return parser.Parse(reader);
      }

      /// <summary>
      /// Runs the parser on the given stream.
      /// </summary>
      public static IEither<T, ParseError> Parse<T>(this IParser<T> parser, Stream input, Encoding encoding)
      {
         IInputReader reader = InputReader.Create(input, encoding);
         return parser.Parse(reader);
      }

      /// <summary>
      /// Applies a projection function to the result of a parser.
      /// </summary>
      public static IParser<TResult> Select<T, TResult>(this IParser<T> parser, Func<T, TResult> func)
      {
         return new SelectParser<T, TResult>(parser, func);
      }

      /// <summary>
      /// Combines the results of two Parsers.
      /// </summary>
      public static IParser<TResult> SelectMany<T, TIntermediate, TResult>(this IParser<T> parser,
                                                                           Func<T, IParser<TIntermediate>> func,
                                                                           Func<T, TIntermediate, TResult> combine)
      {
         return new SelectManyParser<T, TIntermediate, TResult>(parser, func, combine);
      }

      /// <summary>
      /// Tests the given predicate for this parser and returns an error if it fails.
      /// </summary>
      public static IParser<T> Where<T>(this IParser<T> parser, Predicate<T> predicate)
      {
         return new PredicateParser<T>(parser, predicate);
      }
   }
}
