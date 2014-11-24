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
         if (result.IsError())
            Assert.Fail("Expected Success, got: " + result.FromError());
      }

      public static void IsError<TValue>(Either<TValue, string> result)
      {
         if (result.IsSuccess())
            Assert.Fail("Expected Error, got: " + result.FromSuccess());
      }

      public static void ValueEquals<TValue>(TValue expected, Either<TValue, string> result) 
      {
         IsSuccess(result);
         Assert.AreEqual(expected, result.FromSuccess(), "Parsed value");
      }

      public static void ValueEquals<TValue>(IEnumerable<TValue> expected, Either<IEnumerable<TValue>, string> result)
      {
         IsSuccess(result);
         CollectionAssert.AreEqual(expected.ToArray(), result.FromSuccess().ToArray());
      }

      public static void ErrorEquals<TValue>(string expected, Either<TValue, string> result)
      {
         IsError(result);
         Assert.AreEqual(expected, result.FromError(), "Error message");
      }
   }
}
