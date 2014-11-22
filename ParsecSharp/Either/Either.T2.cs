using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Represents values with two possibilities: a value of type Either&lt;S, F&gt; is 
   /// either Success with a value of type S, or Error with a value of type E.
   /// </summary>
   /// <typeparam name="S">The type of the Success value.</typeparam>
   /// <typeparam name="E">The type of the Error value.</typeparam>
   public abstract class Either<S, E>
   {
      /// <summary>
      /// Tests if this instance is a "success" value.
      /// </summary>
      public abstract bool IsSuccess();

      /// <summary>
      /// Tests if this instance is an "error" value.
      /// </summary>
      public abstract bool IsError();

      /// <summary>
      /// Unwraps the success value.
      /// </summary>
      public abstract S FromSuccess();

      /// <summary>
      /// Unwraps the error value.
      /// </summary>
      public abstract E FromError();

      /// <summary>
      /// For a Success value returns the value if the given predicate is true, or the selected Error otherwise.
      /// Always returns an Error for an Error value.
      /// </summary>
      public abstract Either<S, E> Test(Predicate<S> predicate, Func<S, E> selectError);

      public abstract Either<TResult, E> Select<TResult>(Func<S, Either<TResult, E>> func);

      public Either<TResult, E> Select<TResult>(Func<S, TResult> func)
      {
         return Select(x => Either.Success<TResult, E>(func(x)));
      }

      public Either<TResult, E> SelectMany<T, TResult>(Func<S, Either<T, E>> func, Func<S, T, TResult> select)
      {
         return Select(x => func(x).Select(
                       y => select(x, y)));
      }
   }
}
