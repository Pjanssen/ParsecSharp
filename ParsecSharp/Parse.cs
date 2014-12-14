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
      public static Parser<T> Succeed<T>(T value)
      {
         return new SuccessParser<T>(value);
      }

      /// <summary>
      /// Creates a parser that always fails with the given message, without consuming input.
      /// </summary>
      public static Parser<T> Fail<T>(string message)
      {
         return new ErrorParser<T>(message);
      }

      /// <summary>
      /// Tries to parse the input using the given parser, only consuming input when the parser succeeds.
      /// </summary>
      public static Parser<T> Try<T>(Parser<T> parser)
      {
         return new TryParser<T>(parser);
      }

      /// <summary>
      /// Only succeeds when the given parser fails. Returns a Unit.
      /// </summary>
      public static Parser<Unit> Not<T>(Parser<T> parser)
      {
         return new NegatedParser<T>(parser);
      }
   }
}
