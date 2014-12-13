using Microsoft.VisualStudio.TestTools.UnitTesting;
using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.old
{
   [TestClass]
   public class ParseTests
   {
      #region Success

      [TestMethod]
      public void Success_ReturnsGivenValue()
      {
         Parser<char> parser = Parse.Success('x');
         var result = parser.Run("");

         ParseAssert.ValueEquals('x', result);
      }

      #endregion

      #region Eof

      [TestMethod]
      public void Eof_EndOfInput_ReturnsSuccess()
      {
         Parser<Unit> parser = Parse.Eof();
         var result = parser.Run("");

         ParseAssert.IsSuccess(result);
      }

      [TestMethod]
      public void Eof_RemainingInput_ReturnsError()
      {
         Parser<Unit> parser = Parse.Eof();
         var result = parser.Run("abc");

         ParseAssert.IsError(result);
      }

      #endregion

      #region Try

      [TestMethod]
      public void Try_Success_ReturnsSuccess()
      {
         var parser = from a in Parse.Try(Chars.Char('a'))
                      from b in Parse.Try(Chars.Char('b'))
                      select a.ToString() + b.ToString();

         var result = parser.Run("abc");

         ParseAssert.ValueEquals("ab", result);
      }

      [TestMethod]
      public void Try_Error_ReturnsErrorAndResetsPosition()
      {
         var parserA = Parse.Try(Chars.Char('a'));
         var parserB = Parse.Try(Chars.Char('b'));

         IInputReader input = InputReader.Create("bc");
         var resultA = parserA(input);
         ParseAssert.IsError(resultA);

         var resultB = parserB(input);
         ParseAssert.ValueEquals('b', resultB);
      }

      #endregion
   }
}
