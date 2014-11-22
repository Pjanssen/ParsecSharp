using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   internal class Success<S, E> : Either<S, E>
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

      public override bool IsError()
      {
         return false;
      }

      public override S FromSuccess()
      {
         return this.Value;
      }

      public override E FromError()
      {
         throw new InvalidOperationException("Cannot call FromError on Success");
      }

      public override Either<TResult, E> Select<TResult>(Func<S, Either<TResult, E>> func)
      {
         return func(this.Value);
      }

      public override string ToString()
      {
         return "Success (" + this.Value.ToString() + ")";
      }
   }
}
