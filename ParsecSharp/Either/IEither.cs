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
   public interface IEither<out S, out E>
   {
      /// <summary>
      /// Tests if this instance is a "success" value.
      /// </summary>
      bool IsSuccess
      {
         get;
      }

      /// <summary>
      /// Tests if this instance is an "error" value.
      /// </summary>
      bool IsError
      {
         get;
      }

      /// <summary>
      /// Unwraps the success value.
      /// </summary>
      S FromSuccess();

      /// <summary>
      /// Unwraps the error value.
      /// </summary>
      E FromError();
   }
}
