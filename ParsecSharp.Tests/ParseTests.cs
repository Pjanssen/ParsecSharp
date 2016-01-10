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

      #region Choose

      [TestMethod]
      public void Choose_EmptySequence_ReturnsError()
      {
         var parser = Parse.Choose<char>();
         var result = parser.Parse("z");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Choose_UnsuccesfulParsers_ReturnsError()
      {
         var parser = Parse.Choose(Chars.Char('x'), 
                                   Chars.Char('y'),
                                   Chars.Char('z'));
         var result = parser.Parse("a");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Choose_SuccesfulParsers_ReturnsFirstSuccess()
      {
         var parser = Parse.Choose(Chars.Char('x'), 
                                   Chars.Char('y'), 
                                   Chars.Char('z'));
         var result = parser.Parse("y");

         ParseAssert.ValueEquals('y', result);
      }

      #endregion

      #region Chain

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Chain_ParsersNull_ThrowsException()
      {
         Parse.Chain<Unit>(null);
      }

      [TestMethod]
      public void Chain_SucceedingSequence_ReturnsLastResults()
      {
         var parser = Parse.Chain(Chars.Char('x'),
                                  Chars.Char('y'),
                                  Chars.Char('z'));
         var result = parser.Parse("xyz");

         ParseAssert.ValueEquals('z', result);
      }

      [TestMethod]
      public void Chain_FailingSequence_ReturnsError()
      {
         var parser = Parse.Chain(Chars.Char('x'),
                                  Chars.Char('y'),
                                  Chars.Char('z'));
         var result = parser.Parse("xy");

         ParseAssert.IsError(result);
      }

      #endregion

      #region Sequence

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Sequence_ParsersNull_ThrowsException()
      {
         Parse.Sequence<Unit>(null);
      }

      [TestMethod]
      public void Sequence_SucceedingSequence_ReturnsResults()
      {
         var parser = Parse.Sequence(Chars.Char('x'), 
                                     Chars.Char('y'), 
                                     Chars.Char('z'));
         var result = parser.Parse("xyz");

         ParseAssert.ValueEquals(new char[] { 'x', 'y', 'z' }, result);
      }

      [TestMethod]
      public void Sequence_FailingSequence_ReturnsError()
      {
         var parser = Parse.Sequence(Chars.Char('x'), 
                                     Chars.Char('y'), 
                                     Chars.Char('z'));
         var result = parser.Parse("xy");

         ParseAssert.IsError(result);
      }

      #endregion
   }
}
