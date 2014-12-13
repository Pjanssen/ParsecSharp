using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp.old
{
   [TestClass]
   public class ErrorTests
   {
      #region Fail

      [TestMethod]
      public void Fail_ReturnsErrorMessage()
      {
         Parser<char> parser = Error.Fail<char>("test");
         var result = parser.Run("");

         ParseAssert.ErrorEquals("test", result);
      }

      #endregion
   }
}
