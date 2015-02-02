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
         return JsonBoolean | JsonString | JsonNull;
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

      public static readonly Parser<JsonValue> JsonNull =
            from _ in Chars.String("null")
            select (JsonValue)new JsonNull();

      private static readonly Parser<char> EscapedChar =
            Chars.Char('\\') >= (Chars.Char('"') | Chars.Char('\\'));

      public static readonly Parser<string> StringLiteral =
            from open in Chars.Char('"')
            from value in (Parse.Try(EscapedChar) | Chars.Not('"')).Many()
            from close in Chars.Char('"')
            select value;

      public static readonly Parser<JsonValue> JsonString =
            from value in StringLiteral
            select (JsonValue)new JsonString(value);
   }
}
