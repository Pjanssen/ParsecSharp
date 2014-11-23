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
      public static Either<TValue, string> Run<TValue>(this Parser<TValue> parser, string input)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(input, "input");

         ICharStream stream = CharStream.Create(input);
         return parser(stream);
      }

      /// <summary>
      /// Runs the parser with the given input stream.
      /// </summary>
      public static Either<TValue, string> Run<TValue>(this Parser<TValue> parser, System.IO.Stream input, Encoding encoding)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(input, "input");

         ICharStream stream = CharStream.Create(input, encoding);
         return parser(stream);
      }

      public static Parser<TResult> Select<TValue, TResult>(this Parser<TValue> parser, 
                                                            Func<TValue, TResult> func)
      {
         return input =>
         {
            var result = parser(input);
            if (result.IsError())
               return Either.Error<TResult, string>(result.FromError());

            return Either.Success<TResult, string>(func(result.FromSuccess()));
         };
      }

      public static Parser<C> SelectMany<A, B, C>(this Parser<A> parser, 
                                                  Func<A, Parser<B>> func, 
                                                  Func<A, B, C> combine)
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

            if (predicate(result.FromSuccess()))
               return result;

            return Either.Error<TValue, string>("Unexpected " + result.FromSuccess().ToString());
         };
      }

      /// <summary>
      /// Labels the parser with a message that is added to a potential Error value.
      /// </summary>
      public static Parser<TValue> Label<TValue>(this Parser<TValue> parser, string message)
      {
         return Label(parser, () => message);
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

            return Either.Error<TValue, string>(result.FromError() + ". " + msgFunc());
         };
      }
   }
}
