﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class CombineTests
   {
      //#region NotFollowedBy

      //[TestMethod]
      //public void NotFollowedBy_Success_ReturnsError()
      //{
      //   var parser = Combine.NotFollowedBy(Chars.Any());
      //   var result = parser.Parse("xyz");

      //   ParseAssert.ErrorEquals("Unexpected \"x\"", result);
      //}

      //[TestMethod]
      //public void NotFollowedBy_Error_ReturnsSuccess()
      //{
      //   var parser = Combine.NotFollowedBy(Chars.String("xyz"));
      //   var result = parser.Parse("x");

      //   ParseAssert.ValueEquals(Unit.Instance, result);
      //}

      //[TestMethod]
      //public void NotFollowedBy_ParserAError_ReturnsError()
      //{
      //   var parser = Chars.Char('x').NotFollowedBy(Chars.Char('y'));
      //   var result = parser.Parse("y");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void NotFollowedBy_ParserBError_ReturnsParserAValue()
      //{
      //   var parser = Chars.Char('x').NotFollowedBy(Chars.Char('y'));
      //   var result = parser.Parse("xz");

      //   ParseAssert.ValueEquals('x', result);
      //}

      //[TestMethod]
      //public void NotFollowedBy_ParserBSuccess_ReturnsError()
      //{
      //   var parser = Chars.Char('x').NotFollowedBy(Chars.Char('y'));
      //   var result = parser.Parse("xy");

      //   ParseAssert.IsError(result);
      //}

      //#endregion

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

      //#region Choose

      //[TestMethod]
      //public void Choose_EmptySequence_ReturnsError()
      //{
      //   var parser = Combine.Choose<char>();
      //   var result = parser.Parse("z");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void Choose_UnsuccesfulParsers_ReturnsError()
      //{
      //   var parser = Combine.Choose(Chars.Char('x'), Chars.Char('y'), Chars.Char('z'));
      //   var result = parser.Parse("a");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void Choose_SuccesfulParsers_ReturnsFirstSuccess()
      //{
      //   var parser = Combine.Choose(Chars.Char('x'), Chars.Char('y'), Chars.Char('z'));
      //   var result = parser.Parse("y");

      //   ParseAssert.ValueEquals('y', result);
      //}

      //#endregion

      //#region Many

      //[TestMethod]
      //public void Many_NoMatch_ReturnsEmptySet()
      //{
      //   var parser = Combine.Many(Chars.Char('x'));
      //   var result = parser.Parse("y");

      //   ParseAssert.ValueEquals("", result);
      //}

      //[TestMethod]
      //public void Many_OneMatch_ReturnsSetWithOneMatch()
      //{
      //   var parser = Combine.Many(Chars.Char('x'));
      //   var result = parser.Parse("xy");

      //   ParseAssert.ValueEquals("x", result);
      //}

      //[TestMethod]
      //public void Many_ManyMatches_ReturnsSet()
      //{
      //   var parser = Combine.Many(Chars.OneOf("xyz"));
      //   var result = parser.Parse("xxyz0");

      //   ParseAssert.ValueEquals("xxyz", result);
      //}

      //[TestMethod]
      //public void Many_RunMultipleTimes_ReturnsSameResult()
      //{
      //   Parser<string> parser = Chars.Char('x').Many();
      //   var result = parser.Parse("x");
      //   result = parser.Parse("x");

      //   ParseAssert.ValueEquals("x", result);
      //}

      //#endregion

      //#region Many1

      //[TestMethod]
      //public void Many1_NoMatch_ReturnsError()
      //{
      //   var parser = Combine.Many1(Chars.Char('x'));
      //   var result = parser.Parse("y");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void Many1_OneMatch_ReturnsSetWithOneMatch()
      //{
      //   var parser = Combine.Many1(Chars.Char('x'));
      //   var result = parser.Parse("xy");

      //   ParseAssert.ValueEquals("x", result);
      //}

      //[TestMethod]
      //public void Many1_ManyMatches_ReturnsSet()
      //{
      //   var parser = Combine.Many1(Chars.OneOf("xyz"));
      //   var result = parser.Parse("xxyz");

      //   ParseAssert.ValueEquals("xxyz", result);
      //}

      //#endregion

      //#region Between

      //[TestMethod]
      //public void Between_NonMatchingOpen_ReturnsError()
      //{
      //   var parser = Chars.Char('x').Between(Chars.Char('['), Chars.Char(']'));
      //   var result = parser.Parse("_x]");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void Between_NonMatchingClose_ReturnsError()
      //{
      //   var parser = Chars.Char('x').Between(Chars.Char('['), Chars.Char(']'));
      //   var result = parser.Parse("[x_");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void Between_NonMatchingValue_ReturnsError()
      //{
      //   var parser = Chars.Char('x').Between(Chars.Char('['), Chars.Char(']'));
      //   var result = parser.Parse("[_]");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void Between_AllMatching_ReturnsValue()
      //{
      //   var parser = Chars.Any().Between(Chars.Char('['), Chars.Char(']'));
      //   var result = parser.Parse("[x]");

      //   ParseAssert.ValueEquals('x', result);
      //}

      //[TestMethod]
      //public void Between_RepeatingValue_ReturnsValue()
      //{
      //   var parser = Chars.NoneOf("]").Many().Between(Chars.Char('['), Chars.Char(']'));
      //   var result = parser.Parse("[xyz]");

      //   ParseAssert.ValueEquals("xyz", result);
      //}

      //#endregion

      //#region SeparatedBy

      //[TestMethod]
      //public void SeparatedBy_Nothing_ReturnsEmptyValue()
      //{
      //   var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
      //   var result = parser.Parse("");

      //   ParseAssert.ValueEquals(new char[] { }, result);
      //}

      //[TestMethod]
      //public void SeparatedBy_ValueOnly_ReturnsValue()
      //{
      //   var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
      //   var result = parser.Parse("x");

      //   ParseAssert.ValueEquals(new char[] { 'x' }, result);
      //}

      //[TestMethod]
      //public void SeparatedBy_ValueAndSeparator_ReturnsError()
      //{
      //   var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
      //   var result = parser.Parse("x;");

      //   ParseAssert.IsError(result);
      //}

      //[TestMethod]
      //public void SeparatedBy_MultipleValues_ReturnsValues()
      //{
      //   var parser = Chars.Any().SeparatedBy(Chars.Char(';'));
      //   var result = parser.Parse("x;y;z");

      //   ParseAssert.ValueEquals(new char[] { 'x', 'y', 'z' }, result);
      //}

      //#endregion
   }
}
