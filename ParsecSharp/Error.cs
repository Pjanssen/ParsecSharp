using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Defines common methods related to Errors.
   /// </summary>
   public static class Error
   {
      /// <summary>
      /// Creates a new Error with the given message.
      /// </summary>
      public static Either<TValue, string> Create<TValue>(string message)
      {
         return Either.Error<TValue, string>(message);
      }

      /// <summary>
      /// Creates a new Error with the message "Unexpected "x"", where 'x' is the given value.
      /// </summary>
      public static Either<TValue, string> UnexpectedValue<TValue>(object value)
      {
         return Error.Create<TValue>("Unexpected \"" + value.ToString() + "\"");
      }

      /// <summary>
      /// Always fails with the given error message, without consuming any input.
      /// </summary>
      public static Parser<TValue> Fail<TValue>(string message)
      {
         return _ => Error.Create<TValue>(message);
      }

      /// <summary>
      /// Always fails with an "unexpected x" error message, without consuming input.
      /// </summary>
      public static Parser<TValue> UnexpectedParser<TValue>(object value)
      {
         return _ => Error.UnexpectedValue<TValue>(value);
      }
   }
}
