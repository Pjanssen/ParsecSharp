using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class NumericTests
   {
      #region Int

      [TestMethod]
      public void Int_NoDigit_ReturnsError()
      {
         var parser = Numeric.Int();
         var result = parser.Run("xyz");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Int_Digits_ReturnsIntValue()
      {
         var parser = Numeric.Int();
         var result = parser.Run("42xyz");
         
         ParseAssert.ValueEquals(42, result);
      }

      [TestMethod]
      public void Int_Negative_ReturnsIntValue()
      {
         var parser = Numeric.Int();
         var result = parser.Run("-42xyz");

         ParseAssert.ValueEquals(-42, result);
      }

      [TestMethod]
      public void Int_Plus_ReturnIntValue()
      {
         var parser = Numeric.Int();
         var result = parser.Run("+42xyz");

         ParseAssert.ValueEquals(42, result);
      }

      #endregion
   }
}
