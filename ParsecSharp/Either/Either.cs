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
      public static IEither<TResult, E> Select<S, E, TResult>(this IEither<S, E> either, Func<S, IEither<TResult, E>> func)
      {
         if (either.IsSuccess)
            return func(either.FromSuccess());
         else
            return Either.Error<TResult, E>(either.FromError());
      }

      public static IEither<TResult, E> Select<S, E, TResult>(this IEither<S, E> either, Func<S, TResult> func)
      {
         return either.Select(x => Either.Success<TResult, E>(func(x)));
      }

      public static IEither<TResult, E> SelectMany<S, E, T, TResult>(this IEither<S, E> either, Func<S, IEither<T, E>> func, Func<S, T, TResult> select)
      {
         return either.Select(x => func(x).Select(
                              y => select(x, y)));
      }

      /// <summary>
      /// Creates a new Success value.
      /// </summary>
      /// <typeparam name="S">The type of the Success value.</typeparam>
      /// <typeparam name="F">The type of the Error value.</typeparam>
      /// <param name="value">The value to be wrapped.</param>
      public static IEither<S, E> Success<S, E>(S value)
      {
         return new Success<S, E>(value);
      }

      /// <summary>
      /// Creates a new Error value.
      /// </summary>
      /// <typeparam name="S">The type of the Success value.</typeparam>
      /// <typeparam name="E">The type of the Error value.</typeparam>
      /// <param name="value">The value to be wrapped.</param>
      public static IEither<S, E> Error<S, E>(E value)
      {
         return new Error<S, E>(value);
      }
   }
}
