using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   internal sealed class Success<S, E> : IEither<S, E>
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

      public bool IsSuccess
      {
         get
         {
            return true;
         }
      }

      public bool IsError
      {
         get
         {
            return false;
         }
      }

      public S FromSuccess()
      {
         return this.Value;
      }

      public E FromError()
      {
         throw new InvalidOperationException("Cannot call FromError on Success");
      }

      public override string ToString()
      {
         if (this.Value == null)
            return "Success (null)";
         else
            return "Success (" + this.Value.ToString() + ")";
      }
   }
}
