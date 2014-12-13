using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class ParseTests
   {
      #region Succeed

      [TestMethod]
      public void Succeed_ReturnsGivenValue()
      {
         var parser = Parse.Succeed('x');
         var result = parser.Parse("");

         ParseAssert.ValueEquals('x', result);
      }

      #endregion

      #region Fail

      [TestMethod]
      public void Fail_ReturnsParseError()
      {
         var parser = Parse.Fail<int>("test");
         var result = parser.Parse("");

         ParseAssert.ErrorEquals("test", result);
      }

      #endregion
   }
}
