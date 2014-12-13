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
   }
}
