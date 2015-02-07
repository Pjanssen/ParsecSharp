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
      #region Value

      [TestMethod]
      public void CsvUnquotedValue_EmptyString()
      {
         var parser = Parser.Value;
         var result = parser.Parse("");

         ParseAssert.ValueEquals("", result);
      }

      [TestMethod]
      public void CsvUnquotedValue()
      {
         var parser = Parser.Value;
         var result = parser.Parse("test");

         ParseAssert.ValueEquals("test", result);
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

      #endregion

      #region Lines

      [TestMethod]
      public void CsvLines()
      {
         var parser = Parser.Lines;
         var result = parser.Parse("x,42\r\ny,84");

         var row1 = result.FromSuccess().First().ToArray();
         var row2 = result.FromSuccess().Skip(1).First().ToArray();

         CollectionAssert.AreEqual(new string[] { "x", "42" }, row1);
         CollectionAssert.AreEqual(new string[] { "y", "84" }, row2);
      }

      #endregion
   }
}
