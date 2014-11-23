using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Parse
   {
      /// <summary>
      /// Craetes a parser that always returns the given value, regardless of the input.
      /// </summary>
      public static Parser<TValue> Success<TValue>(TValue value)
      {
         return _ => Either.Success<TValue, string>(value);
      }

      /// <summary>
      /// Creates a parser that always returns the given error message, regardless of the input.
      /// </summary>
      public static Parser<TValue> Error<TValue>(string message)
      {
         return _ => Either.Error<TValue, string>(message);
      }

      /// <summary>
      /// Succeeds only at the end of the input.
      /// </summary>
      public static Parser<Unit> Eof()
      {
         return input =>
         {
            if (input.EndOfStream)
               return Either.Success<Unit, string>(Unit.Instance);

            return Either.Error<Unit, string>("Expected end of input");
         };
      }

      /// <summary>
      /// Tries to parse the input using the given parser, resetting the input position if it fails.
      /// </summary>
      public static Parser<TValue> Try<TValue>(Parser<TValue> parser)
      {
         return input =>
         {
            int position = input.Position;

            var result = parser(input);
            if (result.IsError())
               input.Seek(position);

            return result;
         };
      }
   }
}
