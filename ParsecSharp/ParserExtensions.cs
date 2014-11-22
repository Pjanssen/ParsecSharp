using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

         TextReader reader = new StringReader(input);
         return parser(reader);
      }

      /// <summary>
      /// Runs the parser with the given input stream.
      /// </summary>
      public static Either<TValue, string> Run<TValue>(this Parser<TValue> parser, Stream input)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(input, "input");

         TextReader reader = new StreamReader(input);
         return parser(reader);
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
   }
}
