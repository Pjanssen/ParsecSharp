﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp.old
{
   [TestClass]
   public class TokensTests
   {
      #region Lexeme

      [TestMethod]
      public void Lexeme_ConsumesTrailingWhitespace()
      {
         var parser = from x in Tokens.Lexeme(Chars.Char('x'))
                      from y in Chars.Char('y')
                      select x.ToString() + y.ToString();
         var result = parser.Run("x   \ty");

         ParseAssert.ValueEquals("xy", result);
      }

      #endregion
   }
}
