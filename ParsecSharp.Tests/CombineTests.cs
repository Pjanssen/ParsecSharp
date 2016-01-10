using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class CombineTests
   {
      #region Before

      [TestMethod]
      public void Before_ParserAError_ReturnsError()
      {
         var parser = Chars.Char('x').Before(Chars.Char('y'));
         var result = parser.Parse("y");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Before_ParserBError_ReturnsError()
      {
         var parser = Chars.Char('x').Before(Chars.Char('y'));
         var result = parser.Parse("xx");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Before_ParserABSuccess_ReturnsParserBResult()
      {
         var parser = Chars.Char('x').Before(Chars.Char('y'));
         var result = parser.Parse("xy");

         ParseAssert.ValueEquals('y', result);
      }

      #endregion

      #region Between

      [TestMethod]
      public void Between_NonMatchingOpen_ReturnsError()
      {
         var parser = Chars.Char('x').Between(Chars.Char('['), Chars.Char(']'));
         var result = parser.Parse("_x]");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Between_NonMatchingClose_ReturnsError()
      {
         var parser = Chars.Char('x').Between(Chars.Char('['), Chars.Char(']'));
         var result = parser.Parse("[x_");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Between_NonMatchingValue_ReturnsError()
      {
         var parser = Chars.Char('x').Between(Chars.Char('['), Chars.Char(']'));
         var result = parser.Parse("[_]");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Between_AllMatching_ReturnsValue()
      {
         var parser = Chars.Any().Between(Chars.Char('['), Chars.Char(']'));
         var result = parser.Parse("[x]");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Between_RepeatingValue_ReturnsValue()
      {
         var parser = Chars.NoneOf("]").Many().Between(Chars.Char('['), Chars.Char(']'));
         var result = parser.Parse("[xyz]");

         ParseAssert.ValueEquals("xyz", result);
      }

      #endregion

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

      #region FollowedBy

      [TestMethod]
      public void FollowedBy_ParserAError_ReturnsError()
      {
         var parser = Chars.Char('x').FollowedBy(Chars.Char('y'));
         var result = parser.Parse("y");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void FollowedBy_ParserBError_ReturnsError()
      {
         var parser = Chars.Char('x').FollowedBy(Chars.Char('y'));
         var result = parser.Parse("xx");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void FollowedBy_ParserABSuccess_ReturnsParserAResult()
      {
         var parser = Chars.Char('x').FollowedBy(Chars.Char('y'));
         var result = parser.Parse("xy");

         ParseAssert.ValueEquals('x', result);
      }

      #endregion

      #region NotFollowedBy

      [TestMethod]
      public void NotFollowedBy_ParserAError_ReturnsError()
      {
         var parser = Chars.Char('x').NotFollowedBy(Chars.Char('y'));
         var result = parser.Parse("y");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void NotFollowedBy_ParserBError_ReturnsParserAValue()
      {
         var parser = Chars.Char('x').NotFollowedBy(Chars.Char('y'));
         var result = parser.Parse("xz");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void NotFollowedBy_ParserBSuccess_ReturnsError()
      {
         var parser = Chars.Char('x').NotFollowedBy(Chars.Char('y'));
         var result = parser.Parse("xy");

         ParseAssert.IsError(result);
      }

      #endregion

      #region SeparatedBy

      [TestMethod]
      public void SeparatedBy_Nothing_ReturnsEmptyValue()
      {
         var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
         var result = parser.Parse("");

         ParseAssert.ValueEquals(new char[] { }, result);
      }

      [TestMethod]
      public void SeparatedBy_ValueOnly_ReturnsValue()
      {
         var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
         var result = parser.Parse("x");

         ParseAssert.ValueEquals(new char[] { 'x' }, result);
      }

      [TestMethod]
      public void SeparatedBy_ValueAndSeparator_ReturnsError()
      {
         var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
         var result = parser.Parse("x;");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void SeparatedBy_MultipleValues_ReturnsValues()
      {
         var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
         var result = parser.Parse("x;y;z");

         ParseAssert.ValueEquals(new char[] { 'x', 'y', 'z' }, result);
      }

      #endregion

      #region Repeat

      [TestMethod]
      public void Repeat_CountLessThanZero_ReturnsEmptyResult()
      {
         var parser = Chars.Char('x').Repeat(-1);
         var result = parser.Parse("xxxxx");

         ParseAssert.ValueEquals(new char[0], result);
      }

      [TestMethod]
      public void Repeat_CountZero_ReturnsEmptyResult()
      {
         var parser = Chars.Char('x').Repeat(0);
         var result = parser.Parse("xxxxx");

         ParseAssert.ValueEquals(new char[0], result);
      }

      [TestMethod]
      public void Repeat_CountOne_ReturnsSingleMatch()
      {
         var parser = Chars.Char('x').Repeat(1);
         var result = parser.Parse("xxxxx");

         ParseAssert.ValueEquals(new char[] { 'x' }, result);
      }

      [TestMethod]
      public void Repeat_CountThree_ReturnsThreeMatches()
      {
         var parser = Chars.Char('x').Repeat(3);
         var result = parser.Parse("xxxxx");

         ParseAssert.ValueEquals(new char[] { 'x', 'x', 'x' }, result);
      }

      [TestMethod]
      public void Repeat_CountLargerThanMatching_ReturnsError()
      {
         var parser = Chars.Char('x').Repeat(42);
         var result = parser.Parse("xxxxx");

         ParseAssert.IsError(result);
      }

      #endregion
   }
}
