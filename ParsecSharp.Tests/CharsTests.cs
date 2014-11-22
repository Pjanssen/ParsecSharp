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

         Assert.IsTrue(result.IsError());
      }

      [TestMethod]
      public void Any_NonEmptyStream_ReturnsChar()
      {
         Parser<char> parser = Chars.Any();
         var result = parser.Run("a");

         Assert.AreEqual('a', result.FromSuccess());
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

         Assert.AreEqual('x', result.FromSuccess());
      }

      [TestMethod]
      public void Satisfy_FailingPredicate_ReturnsError()
      {
         Parser<char> parser = Chars.Satisfy(c => false);
         var result = parser.Run("xyz");

         Assert.IsTrue(result.IsError());
      }

      #endregion

      #region Char

      [TestMethod]
      public void Char_MatchingChar_ReturnsChar()
      {
         Parser<char> parser = Chars.Char('x');
         var result = parser.Run("xyz");

         Assert.AreEqual('x', result.FromSuccess());
      }

      [TestMethod]
      public void Char_NonMatchingChar_ReturnsChar()
      {
         Parser<char> parser = Chars.Char('x');
         var result = parser.Run("abc");

         Assert.IsTrue(result.IsError());
      }

      #endregion

      #region OneOf

      [TestMethod]
      public void OneOf_MatchingChar_ReturnsChar()
      {
         Parser<char> parser = Chars.OneOf("abc");
         var result = parser.Run("b");

         Assert.AreEqual('b', result.FromSuccess());
      }

      [TestMethod]
      public void OneOf_NonMatchingChar_ReturnsError()
      {
         Parser<char> parser = Chars.OneOf("xyz");
         var result = parser.Run("a");

         Assert.IsTrue(result.IsError());
      }

      #endregion
   }
}
