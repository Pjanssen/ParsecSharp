using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PJanssen.ParsecSharp.IO;

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

      #region Try

      [TestMethod]
      public void Try_Success_ReturnsSuccess()
      {
         var parser = from a in Parse.Try(Parse.Succeed("a"))
                      from b in Parse.Try(Parse.Succeed("b"))
                      select a + b;

         var result = parser.Parse("abc");

         ParseAssert.ValueEquals("ab", result);
      }

      [TestMethod]
      public void Try_Error_ReturnsErrorAndResetsPosition()
      {
         var parser = Parse.Try(from x in Chars.Any()
                                from y in Chars.Any()
                                from f in Parse.Fail<char>("test")
                                select x);

         IInputReader input = InputReader.Create("abc");
         Position expectedPosition = input.GetPosition();

         var result = parser.Parse(input);

         ParseAssert.IsError(result);
         Assert.AreEqual(expectedPosition, input.GetPosition());

      }

      #endregion

      #region Not

      [TestMethod]
      public void Not_Success_ReturnsError()
      {
         var parser = Parse.Not(Chars.Any());
         var result = parser.Parse("xyz");

         ParseAssert.ErrorEquals("Unexpected \"x\"", result);
      }

      [TestMethod]
      public void Not_Error_ReturnsSuccess()
      {
         var parser = Parse.Not(Chars.String("xyz"));
         var result = parser.Parse("x");

         ParseAssert.ValueEquals(Unit.Instance, result);
      }

      #endregion

      #region Eof

      [TestMethod]
      public void Eof_EndOfInput_ReturnsSuccess()
      {
         var parser = Parse.Eof<int>();
         var result = parser.Parse("");

         ParseAssert.IsSuccess(result);
      }

      [TestMethod]
      public void Eof_RemainingInput_ReturnsError()
      {
         var parser = Parse.Eof<int>();
         var result = parser.Parse("abc");

         ParseAssert.IsError(result);
      }

      #endregion
   }
}
