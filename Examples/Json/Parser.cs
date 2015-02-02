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
         return JsonBoolean | JsonNull;
      }

      public static readonly Parser<bool> TrueLiteral =
            from _ in Chars.String("true")
            select true;

      public static readonly Parser<bool> FalseLiteral =
            from _ in Chars.String("false")
            select false;

      public static readonly Parser<JsonValue> JsonBoolean =
            from value in TrueLiteral | FalseLiteral
            select (JsonValue)new JsonBool(value);

      private static readonly Parser<JsonValue> JsonNull =
            from _ in Chars.String("null")
            select (JsonValue)new JsonNull();
   }
}
