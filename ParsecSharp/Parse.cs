using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Parse
   {
      /// <summary>
      /// Creates a parser that always succeeds and returns the given value without consuming input.
      /// </summary>
      public static IParser<T> Succeed<T>(T value)
      {
         return new SuccessParser<T>(value);
      }

      /// <summary>
      /// Creates a parser that always fails with the given message, without consuming input.
      /// </summary>
      public static IParser<T> Fail<T>(string message)
      {
         return new ErrorParser<T>(message);
      }

      /// <summary>
      /// Tries to parse the input using the given parser, only consuming input when the parser succeeds.
      /// </summary>
      public static IParser<T> Try<T>(IParser<T> parser)
      {
         return new TryParser<T>(parser);
      }

      /// <summary>
      /// Only succeeds when the given parser fails. Returns a Unit.
      /// </summary>
      public static IParser<Unit> Not<T>(IParser<T> parser)
      {
         return new NegatedParser<T>(parser);
      }

      /// <summary>
      /// Succeeds only at the end of the input. Returns the default value of T.
      /// </summary>
      public static IParser<T> Eof<T>()
      {
         return new EofParser<T>();
      }
   }
}
