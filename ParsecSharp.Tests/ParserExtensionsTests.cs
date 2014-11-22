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
         Parser<int> parser = from i in Parse.Success<int>(21)
                              select i * 2;

         var result = parser.Run("");

         Assert.AreEqual(42, result.FromSuccess());
      }

      [TestMethod]
      public void Select_Error_ReturnsError()
      {
         Parser<int> parser = from i in Parse.Error<int>("test")
                              select i * 2;

         var result = parser.Run("");

         Assert.AreEqual("test", result.FromError());
      }

      #endregion

      #region SelectMany

      [TestMethod]
      public void SelectMany_Successes_ReturnsSelectedValue()
      {
         Parser<int> parser = from x in Parse.Success(21)
                              from y in Parse.Success(2)
                              select x * y;
         
         var result = parser.Run("");

         Assert.AreEqual(42, result.FromSuccess());
      }

      [TestMethod]
      public void SelectMany_SuccessThenError_ReturnsErrorValue()
      {
         Parser<int> parser = from x in Parse.Success(21)
                              from y in Parse.Error<int>("test")
                              select x * y;

         var result = parser.Run("");

         Assert.AreEqual("test", result.FromError());
      }

      [TestMethod]
      public void SelectMany_ManySuccesses_CombinesAll()
      {
         Parser<int> parser = from w in Parse.Success(21)
                              from x in Parse.Success(3)
                              from y in Parse.Success(4)
                              from z in Parse.Success(6)
                              select (w * x * y) / z;

         var result = parser.Run("");

         Assert.AreEqual(42, result.FromSuccess());
      }

      #endregion
   }
}
