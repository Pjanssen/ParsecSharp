using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   internal class Success<S, F> : Either<S, F>
   {
      public Success(S value)
      {
         this.Value = value;
      }

      public S Value
      {
         get;
         private set;
      }

      public override bool IsSuccess()
      {
         return true;
      }

      public override bool IsFailure()
      {
         return false;
      }

      public override S FromSuccess()
      {
         return this.Value;
      }

      public override F FromFailure()
      {
         throw new InvalidOperationException("Cannot call FromFailure on Success");
      }

      public override Either<TResult, F> Select<TResult>(Func<S, Either<TResult, F>> func)
      {
         return func(this.Value);
      }
   }
}
