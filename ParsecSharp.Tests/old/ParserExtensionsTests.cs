using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.old
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
         Parser<int> parser = from i in Error.Fail<int>("test")
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
                              from y in Error.Fail<int>("test")
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
         var parser = from x in Error.Fail<int>("test")
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

         ParseAssert.ErrorEquals("Unexpected \"42\"", result);
      }

      #endregion

      #region Aggregate

      [TestMethod]
      public void Aggregate_NoMatch_ReturnsSeed()
      {
         var parser = Error.Fail<char>("xyz")
                           .Aggregate(() => "test", (acc, c) => acc + c);
         var result = parser.Run("zz");

         ParseAssert.ValueEquals("test", result);
      }

      [TestMethod]
      public void Aggregate_CombinesParsedValues_UntilError()
      {
         var parser = Chars.OneOf("xy")
                           .Aggregate(() => "test", (acc, c) => acc + c);
         var result = parser.Run("xxyyzz");

         ParseAssert.ValueEquals("testxxyy", result);
      }

      [TestMethod]
      public void Aggregate_ResultSelector_ReturnsTransformedResult()
      {
         var parser = Chars.Digit()
                           .Aggregate(() => "", (acc, c) => acc + c, 
                                                x => int.Parse(x));
         var result = parser.Run("42017");

         ParseAssert.ValueEquals(42017, result);
      }

      [TestMethod]
      public void Aggregate_ConsumedInputThenError_ReturnsError()
      {
         var parser = (from x in Chars.Char('x')
                       from y in Chars.Char('y')
                       select x.ToString() + y.ToString())
                           .Aggregate(() => "", (acc, c) => acc + c);

         var result = parser.Run("xyxz");

         ParseAssert.IsError(result);
      }

      #endregion

      #region Label

      [TestMethod]
      public void Label_Success_ReturnsSuccess()
      {
         var parser = Parse.Success(42).Label(() => "test");
         var result = parser.Run("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Label_Error_ReturnsErrorWithMessage()
      {
         var parser = Error.Fail<int>("Oh noes").Label(() => "Test");
         var result = parser.Run("");

         ParseAssert.ErrorEquals("Oh noes. Test.", result);
      }

      #endregion
   }
}
