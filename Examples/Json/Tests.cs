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
      #region Boolean

      [TestMethod]
      public void BoolTrue()
      {
         var parser = Parser.Create();
         var result = parser.Parse("true");

         ParseAssert.ValueEquals(true, result, r => ((JsonBool)r).Value);
      }

      [TestMethod]
      public void BoolFalse()
      {
         var parser = Parser.Create();
         var result = parser.Parse("false");

         ParseAssert.ValueEquals(false, result, r => ((JsonBool)r).Value);
      }

      #endregion


   }
}
