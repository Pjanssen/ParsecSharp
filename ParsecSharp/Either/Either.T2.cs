using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Represents values with two possibilities: a value of type Either&lt;S, F&gt; is 
   /// either Success with a value of type S, or Failure with a value of type F.
   /// </summary>
   /// <typeparam name="S">The type of the Success value.</typeparam>
   /// <typeparam name="F">The type of the Failure value.</typeparam>
   public abstract class Either<S, F>
   {
      /// <summary>
      /// Tests if this instance is a "success" value.
      /// </summary>
      public abstract bool IsSuccess();

      /// <summary>
      /// Tests if this instance is a "failure" value.
      /// </summary>
      public abstract bool IsFailure();

      /// <summary>
      /// Unwraps the success value.
      /// </summary>
      public abstract S FromSuccess();

      /// <summary>
      /// Unwraps the failure value.
      /// </summary>
      public abstract F FromFailure();

      public abstract Either<TResult, F> Select<TResult>(Func<S, Either<TResult, F>> func);

      public Either<TResult, F> Select<TResult>(Func<S, TResult> func)
      {
         return Select(x => Either.Success<TResult, F>(func(x)));
      }

      public Either<TResult, F> SelectMany<T, TResult>(Func<S, Either<T, F>> func, Func<S, T, TResult> select)
      {
         return Select(x => func(x).Select(
                       y => select(x, y)));
      }
   }
}
