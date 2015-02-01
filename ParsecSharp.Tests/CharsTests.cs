using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PJanssen.ParsecSharp.IO;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class CharsTests
   {
      #region Any

      [TestMethod]
      public void Any_EmptyStream_ReturnsError()
      {
         var parser = Chars.Any();
         var result = parser.Parse("");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Any_NonEmptyStream_ReturnsChar()
      {
         var parser = Chars.Any();
         var result = parser.Parse("a");

         ParseAssert.ValueEquals('a', result);
      }

      #endregion

      #region Satisfy

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Satisfy_PredicateNull_ThrowsException()
      {
         Chars.Satisfy(null);
      }

      [TestMethod]
      public void Satisfy_PassingPredicate_ReturnsChar()
      {
         var parser = Chars.Satisfy(c => true);
         var result = parser.Parse("xyz");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Satisfy_FailingPredicate_ReturnsError()
      {
         var parser = Chars.Satisfy(c => false);
         var result = parser.Parse("xyz");

         ParseAssert.IsError(result);
      }

      #endregion

      #region Char

      [TestMethod]
      public void Char_MatchingChar_ReturnsChar()
      {
         var parser = Chars.Char('x');
         var result = parser.Parse("xyz");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Char_NonMatchingChar_ReturnsError()
      {
         var parser = Chars.Char('x');
         var result = parser.Parse("abc");

         ParseAssert.IsError(result);
      }

      #endregion

      #region OneOf

      [TestMethod]
      public void OneOf_MatchingChar_ReturnsChar()
      {
         var parser = Chars.OneOf("abc");
         var result = parser.Parse("b");

         ParseAssert.ValueEquals('b', result);
      }

      [TestMethod]
      public void OneOf_NonMatchingChar_ReturnsError()
      {
         var parser = Chars.OneOf("xyz");
         var result = parser.Parse("a");

         ParseAssert.IsError(result);
      }

      #endregion

      #region NoneOf

      [TestMethod]
      public void NoneOf_MatchingChar_ReturnsError()
      {
         var parser = Chars.NoneOf("abc");
         var result = parser.Parse("b");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void OneOf_NonMatchingChar_ReturnsChar()
      {
         var parser = Chars.NoneOf("xyz");
         var result = parser.Parse("b");

         ParseAssert.ValueEquals('b', result);
      }

      #endregion

      #region Not

      [TestMethod]
      public void Not_MatchingChar_ReturnsError()
      {
         var parser = Chars.Not('x');
         var result = parser.Parse("x");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Not_NonMatchingChar_ReturnsParsedChar()
      {
         var parser = Chars.Not('x');
         var result = parser.Parse("y");

         ParseAssert.ValueEquals('y', result);
      }

      #endregion

      #region EndOfLine

      [TestMethod]
      public void EndOfLine_NoMatch_ReturnsError()
      {
         var parser = Chars.EndOfLine();
         var result = parser.Parse("abc");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void EndOfLine_LF_ReturnsLF()
      {
         var parser = Chars.EndOfLine();
         var result = parser.Parse("\n");

         ParseAssert.ValueEquals('\n', result);
      }

      [TestMethod]
      public void EndOfLine_CRLF_ReturnsLF()
      {
         var parser = Chars.EndOfLine();
         var result = parser.Parse("\r\n");

         ParseAssert.ValueEquals('\n', result);
      }

      #endregion

      #region String

      [TestMethod]
      public void String_EndOfInput_ReturnsError()
      {
         var parser = Chars.String("xyz");
         var result = parser.Parse("xy");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void String_NoMatch_ReturnsError()
      {
         var parser = Chars.String("xyz");
         var result = parser.Parse("abc");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void String_NoMatch_ConsumesNoInput()
      {
         var parser = Chars.String("xyz");
         var input = new StringInputReader("---");
         var result = parser.Parse(input);

         Position position = input.GetPosition();
         Assert.AreEqual(0, position.Offset);
      }

      [TestMethod]
      public void String_PartialMatch_SetsCorrectPosition()
      {
         var parser = Chars.String("xyz");
         var input = new StringInputReader("xy-");
         var result = parser.Parse(input);

         Position position = input.GetPosition();
         Assert.AreEqual(2, position.Offset, "Offset");
         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(3, position.Column, "Column");
      }

      [TestMethod]
      public void String_Match_ReturnsValue()
      {
         var parser = Chars.String("abc");
         var result = parser.Parse("abc");

         ParseAssert.ValueEquals("abc", result);
      }

      #endregion
   }
}
