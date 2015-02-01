using Json.Syntax;
using PJanssen.ParsecSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json
{
   public static class Parser
   {
      public static Parser<JsonValue> Create()
      {
         return Boolean;
      }

      private static readonly Parser<JsonValue> True =
            from _ in Chars.String("true")
            select (JsonValue)new JsonBool(true);

      private static readonly Parser<JsonValue> False =
            from _ in Chars.String("false")
            select (JsonValue)new JsonBool(false);

      private static readonly Parser<JsonValue> Boolean = True | False;
   }
}
