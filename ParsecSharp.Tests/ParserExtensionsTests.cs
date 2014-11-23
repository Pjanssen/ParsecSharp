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

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Select_Error_ReturnsError()
      {
         Parser<int> parser = from i in Parse.Error<int>("test")
                              select i * 2;

         var result = parser.Run("");

         ParseAssert.ErrorEquals("test", result);
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

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void SelectMany_SuccessThenError_ReturnsErrorValue()
      {
         Parser<int> parser = from x in Parse.Success(21)
                              from y in Parse.Error<int>("test")
                              select x * y;

         var result = parser.Run("");

         ParseAssert.ErrorEquals("test", result);
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

         ParseAssert.ValueEquals(42, result);
      }

      #endregion

      #region Where

      [TestMethod]
      public void Where_PassingPredicate_ReturnsSuccess()
      {
         var parser = from x in Parse.Success(42)
                      where x == 42
                      select x;

         var result = parser.Run("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Where_Error_ReturnsError()
      {
         var parser = from x in Parse.Error<int>("test")
                      where x == 42
                      select x;

         var result = parser.Run("");

         ParseAssert.ErrorEquals("test", result);
      }

      [TestMethod]
      public void Where_FailingPredicate_ReturnsError()
      {
         var parser = from x in Parse.Success(42)
                      where x != 42
                      select x;

         var result = parser.Run("");

         ParseAssert.ErrorEquals("Unexpected 42", result);
      }

      #endregion

      #region Label

      [TestMethod]
      public void Label_Success_ReturnsSuccess()
      {
         var parser = Parse.Success(42).Label("test");
         var result = parser.Run("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Label_Error_ReturnsErrorWithMessage()
      {
         var parser = Parse.Error<int>("Oh noes").Label("Test");
         var result = parser.Run("");

         ParseAssert.ErrorEquals("Oh noes. Test.", result);
      }

      #endregion
   }
}
