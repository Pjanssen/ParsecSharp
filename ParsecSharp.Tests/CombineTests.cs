using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class CombineTests
   {
      #region Or

      [TestMethod]
      public void Or_FirstSuccess_ReturnsFirstResult()
      {
         var parser = Combine.Or(Parse.Success('x'), Parse.Success('y'));
         var result = parser.Run("");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Or_FirstError_ReturnsSecondResult()
      {
         var parser = Combine.Or(Error.Fail<int>("test"), Parse.Success(42));
         var result = parser.Run("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Or_FirstErrorConsumesInput_ReturnsFirstError()
      {
         var parserA = from x in Chars.Any()
                       from y in Error.Fail<char>("test")
                       select y;
         var parserB = Chars.Any();
         var parser = Combine.Or(parserA, parserB);

         var result = parser.Run("abc");

         ParseAssert.ErrorEquals("test", result);
      }

      #endregion

      #region Choose

      [TestMethod]
      public void Choose_EmptySequence_ReturnsError()
      {
         var parser = Combine.Choose<char>();
         var result = parser.Run("z");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Choose_UnsuccesfulParsers_ReturnsError()
      {
         var parser = Combine.Choose(Chars.Char('x'), Chars.Char('y'), Chars.Char('z'));
         var result = parser.Run("a");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Choose_SuccesfulParsers_ReturnsFirstSuccess()
      {
         var parser = Combine.Choose(Chars.Char('x'), Chars.Char('y'), Chars.Char('z'));
         var result = parser.Run("y");

         ParseAssert.ValueEquals('y', result);
      }

      #endregion

      #region Many

      [TestMethod]
      public void Many_NoMatch_ReturnsEmptySet()
      {
         var parser = Combine.Many(Chars.Char('x'));
         var result = parser.Run("y");

         ParseAssert.ValueEquals("", result);
      }

      [TestMethod]
      public void Many_OneMatch_ReturnsSetWithOneMatch()
      {
         var parser = Combine.Many(Chars.Char('x'));
         var result = parser.Run("xy");

         ParseAssert.ValueEquals("x", result);
      }

      [TestMethod]
      public void Many_ManyMatches_ReturnsSet()
      {
         var parser = Combine.Many(Chars.OneOf("xyz"));
         var result = parser.Run("xxyz0");

         ParseAssert.ValueEquals("xxyz", result);
      }

      #endregion

      #region Many1

      [TestMethod]
      public void Many1_NoMatch_ReturnsError()
      {
         var parser = Combine.Many1(Chars.Char('x'));
         var result = parser.Run("y");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Many1_OneMatch_ReturnsSetWithOneMatch()
      {
         var parser = Combine.Many1(Chars.Char('x'));
         var result = parser.Run("xy");

         ParseAssert.ValueEquals("x", result);
      }

      [TestMethod]
      public void Many1_ManyMatches_ReturnsSet()
      {
         var parser = Combine.Many1(Chars.OneOf("xyz"));
         var result = parser.Run("xxyz");

         ParseAssert.ValueEquals("xxyz", result);
      }

      #endregion
   }
}
