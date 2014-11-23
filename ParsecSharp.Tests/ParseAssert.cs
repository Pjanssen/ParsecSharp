using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   internal static class ParseAssert
   {
      public static void IsSuccess<TValue>(Either<TValue, string> result)
      {
         Assert.IsTrue(result.IsSuccess(), "Parser did not return Success");
      }

      public static void IsError<TValue>(Either<TValue, string> result)
      {
         Assert.IsTrue(result.IsError(), "Parser did not return Error");
      }

      public static void ValueEquals<TValue>(TValue expected, Either<TValue, string> result) 
      {
         IsSuccess(result);
         Assert.AreEqual(expected, result.FromSuccess(), "Parsed value");
      }

      public static void ErrorEquals<TValue>(string expected, Either<TValue, string> result)
      {
         IsError(result);
         Assert.AreEqual(expected, result.FromError(), "Error message");
      }
   }
}
