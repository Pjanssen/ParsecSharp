using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   internal class Error<S, E> : Either<S, E>
   {
      public Error(E value)
      {
         this.Value = value;
      }

      public E Value
      {
         get;
         private set;
      }

      public override bool IsSuccess()
      {
         return false;
      }

      public override bool IsError()
      {
         return true;
      }

      public override S FromSuccess()
      {
         throw new InvalidOperationException("Cannot call FromSuccess on Error");
      }

      public override E FromError()
      {
         return this.Value;
      }

      public override Either<TResult, E> Select<TResult>(Func<S, Either<TResult, E>> func)
      {
         return new Error<TResult, E>(this.Value);
      }

      public override string ToString()
      {
         return "Error (" + this.Value.ToString() + ")";
      }
   }
}
