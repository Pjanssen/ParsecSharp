using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Defines static helper methods for the Either type.
   /// </summary>
   public static class Either
   {
      /// <summary>
      /// Creates a new Success value.
      /// </summary>
      /// <typeparam name="S">The type of the Success value.</typeparam>
      /// <typeparam name="F">The type of the Failure value.</typeparam>
      /// <param name="value">The value to be wrapped.</param>
      public static Either<S, F> Success<S, F>(S value)
      {
         return new Success<S, F>(value);
      }

      /// <summary>
      /// Creates a new Failure value.
      /// </summary>
      /// <typeparam name="S">The type of the Success value.</typeparam>
      /// <typeparam name="F">The type of the Failure value.</typeparam>
      /// <param name="value">The value to be wrapped.</param>
      public static Either<S, F> Fail<S, F>(F value)
      {
         return new Failure<S, F>(value);
      }
   }
}
