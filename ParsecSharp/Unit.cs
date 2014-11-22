using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public struct Unit
   {
      private static Unit _Instance = new Unit();

      public static Unit Instance
      {
         get
         {
            return _Instance;
         }
      }
   }
}
