using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Defines Parsers for numeric values.
   /// </summary>
   public static class Numeric
   {
      /// <summary>
      /// Parses an integer value.
      /// </summary>
      public static Parser<int> Int()
      {
         return from x in Chars.Satisfy(c => char.IsDigit(c) || c == '-' || c == '+').Many1()
                select int.Parse(x);
      }
   }
}
