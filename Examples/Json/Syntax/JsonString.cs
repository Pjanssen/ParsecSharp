using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json.Syntax
{
   public class JsonString : JsonValue
   {
      public JsonString(string value)
      {
         this.Value = value;
      }

      public string Value
      {
         get;
         private set;
      }
   }
}
