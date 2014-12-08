using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PJanssen.ParsecSharp.IO;

namespace PJanssen.ParsecSharp
{
   public static class ParserExtensions
   {
      /// <summary>
      /// Runs the parser with the given input string.
      /// </summary>
      public static Either<TValue, ParserError> Run<TValue>(this Parser<TValue> parser, string input)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(input, "input");

         IInputStream stream = CharStream.Create(input);
         return parser(stream);
      }

      /// <summary>
      /// Runs the parser with the given input stream.
      /// </summary>
      public static Either<TValue, ParserError> Run<TValue>(this Parser<TValue> parser, System.IO.Stream input, Encoding encoding)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(input, "input");

         IInputStream stream = CharStream.Create(input, encoding);
         return parser(stream);
      }

      /// <summary>
      /// Applies a projection function to the result of a parser.
      /// </summary>
      public static Parser<TResult> Select<TValue, TResult>(this Parser<TValue> parser, 
                                                            Func<TValue, TResult> func)
      {
         return input =>
         {
            var result = parser(input);
            if (result.IsError())
               return Error.Create<TResult>(result.FromError());

            return Either.Success<TResult, ParserError>(func(result.FromSuccess()));
         };
      }

      /// <summary>
      /// Combines the results of two Parsers.
      /// </summary>
      public static Parser<TResult> SelectMany<TValueA, TValueB, TResult>(this Parser<TValueA> parser, 
                                                                          Func<TValueA, Parser<TValueB>> func, 
                                                                          Func<TValueA, TValueB, TResult> combine)
      {
         return input =>
         {
            return parser(input).Select(resultA => func(resultA)(input).Select(
                                        resultB => combine(resultA, resultB)));
         };
      }

      /// <summary>
      /// Tests the given predicate for this parser and returns an error if it fails.
      /// </summary>
      public static Parser<TValue> Where<TValue>(this Parser<TValue> parser, Predicate<TValue> predicate)
      {
         return input =>
         {
            var result = parser(input);
            if (result.IsError())
               return result;

            TValue resultValue = result.FromSuccess();
            if (predicate(resultValue))
               return result;

            return Error.UnexpectedValue<TValue>(input, resultValue);
         };
      }

      /// <summary>
      /// Applies the parser until it fails, combining the results using the given accumulator function and initial value.
      /// </summary>
      /// <typeparam name="TValue">The type of the parser</typeparam>
      /// <typeparam name="TAccum">The type of the aggregated value</typeparam>
      /// <param name="seed">A function that creates the initial accumulator value</param>
      /// <param name="func">An accumulator function that takes the current accumulated value, the currently parsed result, and combines them into a new accumulated value.</param>
      public static Parser<TAccum> Aggregate<TValue, TAccum>(this Parser<TValue> parser, 
                                                             Func<TAccum> seed, 
                                                             Func<TAccum, TValue, TAccum> func)
      {
         return Aggregate(parser, seed, func, x => x);
      }

      /// <summary>
      /// Applies the parser until it fails, combining the results using the given accumulator function and initial value.
      /// </summary>
      /// <typeparam name="TValue">The type of the parser</typeparam>
      /// <typeparam name="TAccum">The type of the aggregated value</typeparam>
      /// <param name="seed">A function that creates the initial accumulator value</param>
      /// <param name="func">An accumulator function that takes the current accumulated value, the currently parsed result, and combines them into a new accumulated value.</param>
      public static Parser<TResult> Aggregate<TValue, TAccum, TResult>(this Parser<TValue> parser,
                                                                       Func<TAccum> seed,
                                                                       Func<TAccum, TValue, TAccum> func,
                                                                       Func<TAccum, TResult> resultSelector)
      {
         return input =>
         {
            TAccum acc = seed();
            Either<TValue, ParserError> result = null;
            Position position = input.GetPosition();

            while ((result = parser(input)).IsSuccess())
            {
               acc = func(acc, result.FromSuccess());
               position = input.GetPosition();
            }

            if (input.GetPosition() == position)
            {
               TResult accResult = resultSelector(acc);
               return Either.Success<TResult, ParserError>(accResult);
            }
            else
            {
               return Error.Create<TResult>(result.FromError());
            }
         };
      }

      /// <summary>
      /// Labels the parser with a message that is added to a potential Error value.
      /// </summary>
      public static Parser<TValue> Label<TValue>(this Parser<TValue> parser, Func<string> msgFunc)
      {
         return input =>
         {
            var result = parser(input);
            if (result.IsSuccess())
               return result;

            return Error.Create<TValue>(input, result.FromError().Message + ". " + msgFunc() + ".");
         };
      }
   }
}
