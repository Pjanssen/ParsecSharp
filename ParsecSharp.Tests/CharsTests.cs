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
   }
}
