using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public sealed class Unit
   {
      private Unit() { }

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
