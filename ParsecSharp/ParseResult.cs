using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class ParseResult
   {
      public static Either<T, ParseError> Success<T>(T value)
      {
         return Either.Success<T, ParseError>(value);
      }

      public static Either<T, ParseError> Error<T>(IInputReader input, string message)
      {
         return Either.Error<T, ParseError>(new ParseError(input.GetPosition(), message));
      }
   }
}
