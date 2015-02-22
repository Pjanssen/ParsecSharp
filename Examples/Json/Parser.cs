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
      public static readonly IParser<bool> TrueLiteral =
            from _ in Chars.String("true")
            select true;

      public static readonly IParser<bool> FalseLiteral =
            from _ in Chars.String("false")
            select false;

      public static readonly IParser<JsonValue> JsonBoolean =
            from value in Tokens.Lexeme(TrueLiteral.Or(FalseLiteral))
            select new JsonBool(value);


      public static readonly IParser<JsonValue> JsonNull =
            from _ in Tokens.Symbol("null")
            select new JsonNull();

      
      private static readonly IParser<char> EscapedChar =
            Chars.Char('\\').Before(Chars.OneOf("\"\\"));

      public static readonly IParser<string> StringLiteral =
            from open in Chars.Char('"')
            from value in (EscapedChar.Try().Or(Chars.Not('"'))).Many()
            from close in Chars.Char('"')
            select value;

      public static readonly IParser<JsonValue> JsonString =
            from value in Tokens.Lexeme(StringLiteral)
            select new JsonString(value);


      public static readonly IParser<double> Number = Numeric.Double();

      public static readonly IParser<JsonValue> JsonNumber =
            from value in Tokens.Lexeme(Number)
            select new JsonNumber(value);


      public static readonly IParser<IEnumerable<JsonValue>> Array =
            from open in Tokens.Symbol('[')
            from values in JsonValue.SeparatedBy(Tokens.Symbol(','))
            from close in Tokens.Symbol(']')
            select values;

      public static readonly IParser<JsonValue> JsonArray =
            from values in Array
            select new JsonArray(values);

      
      private static readonly IParser<KeyValuePair<string, JsonValue>> ObjectEntry =
            from key in Tokens.Lexeme(StringLiteral)
            from sep in Tokens.Symbol(':')
            from value in JsonValue
            select new KeyValuePair<string, JsonValue>(key, value);

      public static readonly IParser<Dictionary<string, JsonValue>> Object =
            from open in Tokens.Symbol('{')
            from values in ObjectEntry.SeparatedBy(Tokens.Symbol(','))
            from close in Tokens.Symbol('}')
            select values.ToDictionary(v => v.Key, v => v.Value);

      public static readonly IParser<JsonValue> JsonObject =
            from values in Object
            select new JsonObject(values);


      public static readonly IParser<JsonValue> JsonValue = Combine.Choose( JsonObject
                                                                          , JsonArray
                                                                          , JsonString
                                                                          , JsonNumber
                                                                          , JsonBoolean
                                                                          , JsonNull);
   }
}
