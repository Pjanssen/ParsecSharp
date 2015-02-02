using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json.Syntax
{
   public class JsonNumber : JsonValue
   {
      public JsonNumber(double value)
      {
         this.Value = value;
      }

      public double Value
      {
         get;
         private set;
      }
   }
}
