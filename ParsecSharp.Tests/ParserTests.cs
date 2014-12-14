﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class ParserTests
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

      #region SelectMany

      [TestMethod]
      public void SelectMany_Successes_ReturnsSelectedValue()
      {
         var parser = from x in Parse.Succeed(21)
                      from y in Parse.Succeed(2)
                      select x * y;

         var result = parser.Parse("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void SelectMany_SuccessThenError_ReturnsErrorValue()
      {
         var parser = from x in Parse.Succeed(21)
                      from y in Parse.Fail<int>("test")
                      select x * y;

         var result = parser.Parse("");

         ParseAssert.ErrorEquals("test", result);
      }

      [TestMethod]
      public void SelectMany_ManySuccesses_CombinesAll()
      {
         var parser = from w in Parse.Succeed(21)
                      from x in Parse.Succeed(3)
                      from y in Parse.Succeed(4)
                      from z in Parse.Succeed(6)
                      select (w * x * y) / z;

         var result = parser.Parse("");

         ParseAssert.ValueEquals(42, result);
      }

      #endregion

      #region Where

      [TestMethod]
      public void Where_PassingPredicate_ReturnsSuccess()
      {
         var parser = from x in Parse.Succeed(42)
                      where x == 42
                      select x;

         var result = parser.Parse("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Where_Error_ReturnsError()
      {
         var parser = from x in Parse.Fail<int>("test")
                      where x == 42
                      select x;

         var result = parser.Parse("");

         ParseAssert.ErrorEquals("test", result);
      }

      [TestMethod]
      public void Where_FailingPredicate_ReturnsError()
      {
         var parser = from x in Parse.Succeed(42)
                      where x != 42
                      select x;

         var result = parser.Parse("");

         ParseAssert.ErrorEquals("Unexpected \"42\"", result);
      }

      #endregion

      #region Label

      [TestMethod]
      public void Label_Success_ReturnsSuccess()
      {
         var parser = Parse.Succeed(42).Label(() => "test");
         var result = parser.Parse("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Label_Error_ReturnsErrorWithMessage()
      {
         var parser = Parse.Fail<int>("Oh noes").Label(() => "Test");
         var result = parser.Parse("");

         ParseAssert.ErrorEquals("Oh noes. Test.", result);
      }

      #endregion
   }
}
