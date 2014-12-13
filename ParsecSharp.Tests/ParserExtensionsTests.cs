using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class ParserExtensionsTests
   {
      #region Select

      [TestMethod]
      public void Select_Success_ReturnsSelectedValue()
      {
         var parser = from i in Parse.Succeed(21)
                      select i * 2;

         var result = parser.Parse("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Select_Error_ReturnsError()
      {
         Parser<int> parser = from i in Parse.Fail<int>("test")
                              select i * 2;

         var result = parser.Parse("");

         ParseAssert.ErrorEquals("test", result);
      }

      #endregion
   }
}
