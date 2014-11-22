using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Throw
   {
      public static void IfNull(object value, string paramName)
      {
         if (value == null)
            throw new ArgumentNullException(paramName);
      }
   }
}
