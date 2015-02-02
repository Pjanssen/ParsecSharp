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
         var parser = Parser.Create();
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
   }
}
