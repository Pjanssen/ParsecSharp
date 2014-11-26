using PJanssen.ParsecSharp.IO;
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
      public static Either<TValue, ParserError> Create<TValue>(ICharStream input, string message)
      {
         ParserError parserError = new ParserError(input.Line, input.Column, message);
         return Create<TValue>(parserError);
      }

      public static Either<T, ParserError> Create<T>(ParserError parserError)
      {
         return Either.Error<T, ParserError>(parserError);
      }

      /// <summary>
      /// Creates a new Error with the message "Unexpected "x"", where 'x' is the given value.
      /// </summary>
      public static Either<TValue, ParserError> UnexpectedValue<TValue>(ICharStream input, object value)
      {
         return Error.Create<TValue>(input, "Unexpected \"" + value.ToString() + "\"");
      }

      /// <summary>
      /// Always fails with the given error message, without consuming any input.
      /// </summary>
      public static Parser<TValue> Fail<TValue>(string message)
      {
         return input => Error.Create<TValue>(input, message);
      }

      /// <summary>
      /// Always fails with an "unexpected x" error message, without consuming input.
      /// </summary>
      public static Parser<TValue> UnexpectedParser<TValue>(object value)
      {
         return input => Error.UnexpectedValue<TValue>(input, value);
      }
   }
}
