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
         return JsonValue;
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
            Chars.Char('\\') >= Chars.OneOf("\"\\");

      public static readonly Parser<string> StringLiteral =
            from open in Chars.Char('"')
            from value in (Parse.Try(EscapedChar) | Chars.Not('"')).Many()
            from close in Chars.Char('"')
            select value;

      public static readonly Parser<JsonValue> JsonString =
            from value in StringLiteral
            select (JsonValue)new JsonString(value);

      public static readonly Parser<IEnumerable<JsonValue>> Array =
            from open in Tokens.Symbol('[')
            from values in Tokens.Lexeme(JsonValue).SeparatedBy(Tokens.Symbol(','))
            from close in Tokens.Symbol(']')
            select values;

      public static readonly Parser<JsonValue> JsonArray =
            from values in Array
            select (JsonValue)new JsonArray(values);

      private static readonly Parser<KeyValuePair<string, JsonValue>> ObjectEntry =
            from key in StringLiteral
            from sep in Tokens.Symbol(':')
            from value in Tokens.Lexeme(JsonValue)
            select new KeyValuePair<string, JsonValue>(key, value);

      public static readonly Parser<Dictionary<string, JsonValue>> Object =
            from open in Tokens.Symbol('{')
            from values in ObjectEntry.SeparatedBy(Tokens.Symbol(','))
            from close in Tokens.Symbol('}')
            select values.ToDictionary(v => v.Key, v => v.Value);

      public static readonly Parser<JsonValue> JsonObject =
            from values in Object
            select (JsonValue)new JsonObject(values);

      public static readonly Parser<JsonValue> JsonValue = JsonObject
                                                         | JsonArray
                                                         | JsonBoolean 
                                                         | JsonString 
                                                         | JsonNull;
   }
}
