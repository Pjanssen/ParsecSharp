using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.old
{
   internal static class ParseAssert
   {
      public static void IsSuccess<TValue>(Either<TValue, ParseError> result)
      {
         if (result.IsError())
            Assert.Fail("Expected Success, got: " + result.FromError());
      }

      public static void IsError<TValue>(Either<TValue, ParseError> result)
      {
         if (result.IsSuccess())
            Assert.Fail("Expected Error, got: " + result.FromSuccess());
      }

      public static void ValueEquals<TValue>(TValue expected, Either<TValue, ParseError> result) 
      {
         IsSuccess(result);
         Assert.AreEqual(expected, result.FromSuccess(), "Parsed value");
      }

      public static void ValueEquals<TValue>(IEnumerable<TValue> expected, Either<IEnumerable<TValue>, ParseError> result)
      {
         IsSuccess(result);
         CollectionAssert.AreEqual(expected.ToArray(), result.FromSuccess().ToArray());
      }

      public static void ErrorEquals<TValue>(string expected, Either<TValue, ParseError> result)
      {
         IsError(result);
         Assert.AreEqual(expected, result.FromError().Message, "Error message");
      }
   }
}
