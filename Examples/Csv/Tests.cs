using Csv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PJanssen.ParsecSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csv
{
   [TestClass]
   public class Tests
   {
      #region Unquoted Value

      [TestMethod]
      public void CsvUnquotedValue_EmptyString()
      {
         var parser = Parser.UnquotedValue;
         var result = parser.Parse("");

         ParseAssert.ValueEquals("", result);
      }

      [TestMethod]
      public void CsvUnquotedValue()
      {
         var parser = Parser.UnquotedValue;
         var result = parser.Parse("test");

         ParseAssert.ValueEquals("test", result);
      }

      #endregion

      #region Quoted Value

      [TestMethod]
      public void CsvQuotedValue()
      {
         var parser = Parser.QuotedValue;
         var result = parser.Parse(@"""test""");

         ParseAssert.ValueEquals("test", result);
      }

      [TestMethod]
      public void CsvQuotedValue_EscapedQuote()
      {
         var parser = Parser.QuotedValue;
         var result = parser.Parse(@"""te""""st""");

         ParseAssert.ValueEquals(@"te""st", result);
      }

      [TestMethod]
      public void CsvQuotedValue_SeparatorValue()
      {
         var parser = Parser.QuotedValue;
         var result = parser.Parse(@"""t,e,s,t""");

         ParseAssert.ValueEquals("t,e,s,t", result);
      }

      #endregion

      #region Line

      [TestMethod]
      public void CsvLine()
      {
         var parser = Parser.Line;
         var result = parser.Parse("test,42,x");

         ParseAssert.ValueEquals(new string[] { "test", "42", "x" }, result);
      }

      [TestMethod]
      public void CsvLine_SeparatorsOnly()
      {
         var parser = Parser.Line;
         var result = parser.Parse(",,");

         ParseAssert.ValueEquals(new string[] { "", "", "" }, result);
      }

      [TestMethod]
      public void CsvLine_QuotedValues()
      {
         var parser = Parser.Line;
         var result = parser.Parse(@"""test"",""42"",""x""");

         ParseAssert.ValueEquals(new string[] { "test", "42", "x" }, result);
      }

      [TestMethod]
      public void CsvLine_MixedQuotedUnquotedValues()
      {
         var parser = Parser.Line;
         var result = parser.Parse(@"""test"",42,""x""");

         ParseAssert.ValueEquals(new string[] { "test", "42", "x" }, result);
      }

      #endregion

      #region Lines

      [TestMethod]
      public void CsvLines()
      {
         var parser = Parser.Lines;
         var result = parser.Parse("x,42\r\ny,84");

         var rows = result.FromSuccess().ToArray();
         var row1 = rows[0].ToArray();
         var row2 = rows[1].ToArray();

         CollectionAssert.AreEqual(new string[] { "x", "42" }, row1);
         CollectionAssert.AreEqual(new string[] { "y", "84" }, row2);
      }

      [TestMethod]
      public void CsvLines_QuotedValues()
      {
         var parser = Parser.Lines;
         var result = parser.Parse("\"x\"\r\n\"y\"");

         var rows = result.FromSuccess().ToArray();
         var row1 = rows[0].ToArray();
         var row2 = rows[1].ToArray();

         CollectionAssert.AreEqual(new string[] { "x" }, row1);
         CollectionAssert.AreEqual(new string[] { "y" }, row2);
      }

      [TestMethod]
      public void CsvLines_QuotedValueNewlines()
      {
         var parser = Parser.Lines;
         var result = parser.Parse("\"x\",\"42\"\r\n\"y\r\nz\",\"84\"");

         var rows = result.FromSuccess().ToArray();
         var row1 = rows[0].ToArray();
         var row2 = rows[1].ToArray();

         CollectionAssert.AreEqual(new string[] { "x", "42" }, row1);
         CollectionAssert.AreEqual(new string[] { "y\r\nz", "84" }, row2);
      }

      #endregion
   }
}
