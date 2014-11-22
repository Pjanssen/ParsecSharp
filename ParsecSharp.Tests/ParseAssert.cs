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
         Assert.IsTrue(result.IsSuccess());
      }

      public static void IsError<TValue>(Either<TValue, string> result)
      {
         Assert.IsTrue(result.IsError());
      }

      public static void ValueEquals<TValue>(TValue expected, Either<TValue, string> result) 
      {
         Assert.IsTrue(result.IsSuccess());
         Assert.AreEqual(expected, result.FromSuccess());
      }

      public static void ErrorEquals<TValue>(string expected, Either<TValue, string> result)
      {
         Assert.IsTrue(result.IsError());
         Assert.AreEqual(expected, result.FromError());
      }
   }
}
