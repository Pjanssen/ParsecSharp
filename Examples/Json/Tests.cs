using Json.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PJanssen.ParsecSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Json
{
   [TestClass]
   public class Tests
   {
      #region Null

      [TestMethod]
      public void JsonNull()
      {
         var parser = Parser.JsonNull;
         var result = parser.Parse("null");

         ParseAssert.ValueEquals(typeof(JsonNull), result, v => v.GetType());
      }

      #endregion

      #region Boolean

      [TestMethod]
      public void TrueLiteral()
      {
         var parser = Parser.TrueLiteral;
         var result = parser.Parse("true");

         ParseAssert.ValueEquals(true, result);
      }

      [TestMethod]
      public void FalseLiteral()
      {
         var parser = Parser.FalseLiteral;
         var result = parser.Parse("false");

         ParseAssert.ValueEquals(false, result);
      }

      [TestMethod]
      public void JsonBool_False()
      {
         var parser = Parser.JsonBoolean;
         var result = parser.Parse("false");

         ParseAssert.ValueEquals(false, result, r => ((JsonBool)r).Value);
      }

      #endregion

      #region String

      [TestMethod]
      public void JsonString()
      {
         var parser = Parser.StringLiteral;
         var result = parser.Parse("\"test\"");

         ParseAssert.ValueEquals("test", result);
      }

      [TestMethod]
      public void JsonString_EscapedQuote()
      {
         var parser = Parser.StringLiteral;
         var result = parser.Parse("\"\\\"escaped quotes\\\"\"");

         ParseAssert.ValueEquals("\"escaped quotes\"", result);
      }

      [TestMethod]
      public void JsonString_EscapedSlash()
      {
         var parser = Parser.StringLiteral;
         var result = parser.Parse(@"""\\""");

         ParseAssert.ValueEquals("\\", result);
      }

      #endregion

      #region Array

      [TestMethod]
      public void JsonArray_EmptyArray()
      {
         var parser = Parser.Array;
         var result = parser.Parse("[]");

         ParseAssert.ValueEquals(new JsonValue[0], result);
      }

      [TestMethod]
      public void JsonArray_SingleValue()
      {
         var parser = Parser.Array;
         var result = parser.Parse("[true]");

         ParseAssert.ValueEquals(true, result, v => ((JsonBool)v.First()).Value);
      }

      [TestMethod]
      public void JsonArray_MultipleValues()
      {
         var parser = Parser.Array;
         var result = parser.Parse("[true,false,true]");

         ParseAssert.ValueEquals(true, result, v => ((JsonBool)v.First()).Value);
         ParseAssert.ValueEquals(false, result, v => ((JsonBool)v.Skip(1).First()).Value);
         ParseAssert.ValueEquals(true, result, v => ((JsonBool)v.Skip(2).First()).Value);
      }

      [TestMethod]
      public void JsonArray_WithWhitespace()
      {
         var parser = Parser.Array;
         var result = parser.Parse(@"[ true
                                     , false ]");

         ParseAssert.ValueEquals(true, result, v => ((JsonBool)v.First()).Value);
         ParseAssert.ValueEquals(false, result, v => ((JsonBool)v.Skip(1).First()).Value);
      }

      [TestMethod]
      public void JsonArray_NestedArray()
      {
         var parser = Parser.Array;
         var result = parser.Parse(@"[ true, [ false, false ] ]");

         ParseAssert.ValueEquals(true, result, v => ((JsonBool)v.First()).Value);
         ParseAssert.ValueEquals(2, result, v => ((JsonArray)v.Skip(1).First()).Values.Count());
      }

      #endregion

      #region Object

      [TestMethod]
      public void JsonObject_EmptyObject()
      {
         var parser = Parser.Object;
         var result = parser.Parse("{}");

         ParseAssert.ValueEquals(0, result, v => v.Count);
      }

      [TestMethod]
      public void JsonObject_SingleProperty()
      {
         var parser = Parser.Object;
         var result = parser.Parse(@"{ ""test"": ""42"" }");

         ParseAssert.ValueEquals("42", result, v => ((JsonString)v["test"]).Value);
      }

      [TestMethod]
      public void JsonObject_MultipleProperties()
      {
         var parser = Parser.Object;
         var result = parser.Parse(@"{ ""test"": ""42"", ""x"": true }");

         ParseAssert.ValueEquals("42", result, v => ((JsonString)v["test"]).Value);
         ParseAssert.ValueEquals(true, result, v => ((JsonBool)v["x"]).Value);
      }

      [TestMethod]
      public void JsonObject_NestedObjects()
      {
         var parser = Parser.Object;
         var result = parser.Parse(@"{ ""x"" : { ""y"": true } }");

         ParseAssert.ValueEquals(true, result, v => ((JsonBool)((JsonObject)v["x"]).Values["y"]).Value);
      }

      #endregion
   }
}
