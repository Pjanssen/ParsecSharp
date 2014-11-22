using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class ParseTests
   {
      #region Success

      [TestMethod]
      public void Success_ReturnsGivenValue()
      {
         Parser<char> parser = Parse.Success('x');
         var result = parser.Run("");

         Assert.AreEqual('x', result.FromSuccess());
      }

      #endregion

      #region Error

      [TestMethod]
      public void Error_ReturnsErrorMessage()
      {
         Parser<char> parser = Parse.Error<char>("test");
         var result = parser.Run("");

         Assert.AreEqual("test", result.FromError());
      }

      #endregion
   }
}
