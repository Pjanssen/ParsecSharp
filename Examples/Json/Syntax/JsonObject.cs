using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json.Syntax
{
   public class JsonObject : JsonValue
   {
      public JsonObject(Dictionary<string, JsonValue> values)
      {
         this.Values = values;
      }

      public Dictionary<string, JsonValue> Values
      {
         get;
         private set;
      }
   }
}
