using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json.Syntax
{
   public class JsonArray : JsonValue
   {
      public JsonArray(IEnumerable<JsonValue> values)
      {
         this.Values = values;
      }

      public IEnumerable<JsonValue> Values
      {
         get;
         private set;
      }
   }
}
