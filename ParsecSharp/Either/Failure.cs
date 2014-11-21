using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   internal class Failure<S, F> : Either<S, F>
   {
      public Failure(F value)
      {
         this.Value = value;
      }

      public F Value
      {
         get;
         private set;
      }

      public override bool IsSuccess()
      {
         return false;
      }

      public override bool IsFailure()
      {
         return true;
      }

      public override S FromSuccess()
      {
         throw new InvalidOperationException("Cannot call FromSuccess on Failure");
      }

      public override F FromFailure()
      {
         return this.Value;
      }

      public override Either<TResult, F> Select<TResult>(Func<S, Either<TResult, F>> func)
      {
         return new Failure<TResult, F>(this.Value);
      }
   }
}
