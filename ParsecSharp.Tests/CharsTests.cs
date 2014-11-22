using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class CharsTests
   {
      #region Any

      [TestMethod]
      public void Any_EmptyStream_ReturnsError()
      {
         Parser<char> parser = Chars.Any();
         var result = parser.Run("");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Any_NonEmptyStream_ReturnsChar()
      {
         Parser<char> parser = Chars.Any();
         var result = parser.Run("a");

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
         Parser<char> parser = Chars.Satisfy(c => true);
         var result = parser.Run("xyz");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Satisfy_FailingPredicate_ReturnsError()
      {
         Parser<char> parser = Chars.Satisfy(c => false);
         var result = parser.Run("xyz");

         ParseAssert.IsError(result);
      }

      #endregion

      #region Char

      [TestMethod]
      public void Char_MatchingChar_ReturnsChar()
      {
         Parser<char> parser = Chars.Char('x');
         var result = parser.Run("xyz");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Char_NonMatchingChar_ReturnsChar()
      {
         Parser<char> parser = Chars.Char('x');
         var result = parser.Run("abc");

         ParseAssert.IsError(result);
      }

      #endregion

      #region OneOf

      [TestMethod]
      public void OneOf_MatchingChar_ReturnsChar()
      {
         Parser<char> parser = Chars.OneOf("abc");
         var result = parser.Run("b");

         ParseAssert.ValueEquals('b', result);
      }

      [TestMethod]
      public void OneOf_NonMatchingChar_ReturnsError()
      {
         Parser<char> parser = Chars.OneOf("xyz");
         var result = parser.Run("a");

         ParseAssert.IsError(result);
      }

      #endregion
   }
}
