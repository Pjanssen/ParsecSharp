using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Parse
   {
      /// <summary>
      /// Always succeeds with the given value, without consuming any input.
      /// </summary>
      public static Parser<TValue> Success<TValue>(TValue value)
      {
         return _ => Either.Success<TValue, ParserError>(value);
      }

      /// <summary>
      /// Succeeds only at the end of the input.
      /// </summary>
      public static Parser<Unit> Eof()
      {
         return input =>
         {
            if (input.EndOfStream)
               return Either.Success<Unit, ParserError>(Unit.Instance);

            return Error.Create<Unit>(input, "Expected end of input");
         };
      }

      /// <summary>
      /// Tries to parse the input using the given parser, only consuming input when the parser succeeds.
      /// </summary>
      public static Parser<TValue> Try<TValue>(Parser<TValue> parser)
      {
         return input =>
         {
            Position position = input.GetPosition();

            var result = parser(input);
            if (result.IsError())
               input.Seek(position);

            return result;
         };
      }
   }
}
