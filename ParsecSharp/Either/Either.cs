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
      /// <typeparam name="F">The type of the Error value.</typeparam>
      /// <param name="value">The value to be wrapped.</param>
      public static Either<S, E> Success<S, E>(S value)
      {
         return new Success<S, E>(value);
      }

      /// <summary>
      /// Creates a new Error value.
      /// </summary>
      /// <typeparam name="S">The type of the Success value.</typeparam>
      /// <typeparam name="E">The type of the Error value.</typeparam>
      /// <param name="value">The value to be wrapped.</param>
      public static Either<S, E> Error<S, E>(E value)
      {
         return new Error<S, E>(value);
      }
   }
}
