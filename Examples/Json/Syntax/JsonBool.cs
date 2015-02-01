using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json.Syntax
{
   public class JsonBool : JsonValue
   {
      public JsonBool(bool value)
      {
         Value = value;
      }

      public bool Value
      {
         get;
         private set;
      }
   }
}
