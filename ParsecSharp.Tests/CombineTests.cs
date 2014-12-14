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
         var parser = Combine.Or(Parse.Succeed('x'), Parse.Succeed('y'));
         var result = parser.Parse("");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Or_FirstError_ReturnsSecondResult()
      {
         var parser = Combine.Or(Parse.Fail<int>("test"), Parse.Succeed(42));
         var result = parser.Parse("");

         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Or_FirstErrorConsumesInput_ReturnsFirstError()
      {
         var parserA = from x in Chars.Any()
                       from y in Parse.Fail<char>("test")
                       select y;
         var parserB = Chars.Any();
         var parser = Combine.Or(parserA, parserB);

         var result = parser.Parse("abc");

         ParseAssert.ErrorEquals("test", result);
      }

      #endregion

      #region Many

      [TestMethod]
      public void Many_NoMatch_ReturnsEmptySet()
      {
         var parser = Chars.Char('x').Many();
         var result = parser.Parse("y");

         ParseAssert.ValueEquals("", result);
      }

      [TestMethod]
      public void Many_OneMatch_ReturnsSetWithOneMatch()
      {
         var parser = Chars.Char('x').Many();
         var result = parser.Parse("xy");
         
         ParseAssert.ValueEquals("x", result);
      }

      [TestMethod]
      public void Many_ManyMatches_ReturnsSet()
      {
         var parser = Chars.OneOf("xyz").Many();
         var result = parser.Parse("xxyz0");

         ParseAssert.ValueEquals("xxyz", result);
      }

      [TestMethod]
      public void Many_RunMultipleTimes_ReturnsSameResult()
      {
         var parser = Chars.Char('x').Many();
         var result = parser.Parse("x");
         result = parser.Parse("x");

         ParseAssert.ValueEquals("x", result);
      }

      [TestMethod]
      public void Many_IEnumerable()
      {
         var parser = (from c in Chars.Any() 
                       from x in Parse.Succeed(42) 
                       select x).Many();
         var result = parser.Parse("xyz");

         ParseAssert.ValueEquals(new[] { 42, 42, 42 }, result);
      }

      #endregion

      #region Many1

      [TestMethod]
      public void Many1_NoMatch_ReturnsError()
      {
         var parser = Chars.Char('x').Many1();
         var result = parser.Parse("y");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Many1_OneMatch_ReturnsSetWithOneMatch()
      {
         var parser = Chars.Char('x').Many1();
         var result = parser.Parse("xy");

         ParseAssert.ValueEquals("x", result);
      }

      [TestMethod]
      public void Many1_ManyMatches_ReturnsSet()
      {
         var parser = Chars.OneOf("xyz").Many1();
         var result = parser.Parse("xxyz");

         ParseAssert.ValueEquals("xxyz", result);
      }

      #endregion

   }
}
