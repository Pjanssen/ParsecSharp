using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   internal sealed class Error<S, E> : IEither<S, E>
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

      public bool IsSuccess()
      {
         return false;
      }

      public bool IsError()
      {
         return true;
      }

      public S FromSuccess()
      {
         throw new InvalidOperationException("Cannot call FromSuccess on Error");
      }

      public E FromError()
      {
         return this.Value;
      }

      public override string ToString()
      {
         if (this.Value == null)
            return "Error (null)";
         else
            return "Error (" + this.Value.ToString() + ")";
      }
   }
}
