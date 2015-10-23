using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class ParseAssert
   {
      public static void IsSuccess<TValue>(IEither<TValue, ParseError> result)
      {
         if (result.IsError)
            Assert.Fail("Expected Success, got: " + result.FromError());
      }

      public static void IsError<TValue>(IEither<TValue, ParseError> result)
      {
         if (result.IsSuccess)
            Assert.Fail("Expected Error, got: " + result.FromSuccess());
      }

      public static void ValueEquals<TValue>(TValue expected, IEither<TValue, ParseError> result) 
      {
         ValueEquals(expected, result, v => v);
      }

      public static void ValueEquals<T, TValue>(TValue expected, IEither<T, ParseError> result, Func<T, TValue> valueSelector)
      {
         IsSuccess(result);
         Assert.AreEqual(expected, valueSelector(result.FromSuccess()), "Parsed value");
      }

      public static void ValueEquals(double expected, IEither<double, ParseError> result)
      {
         IsSuccess(result);
         Assert.AreEqual(expected, result.FromSuccess(), 0.0001);
      }

      public static void ValueEquals<TValue>(IEnumerable<TValue> expected, IEither<IEnumerable<TValue>, ParseError> result)
      {
         IsSuccess(result);
         CollectionAssert.AreEqual(expected.ToArray(), result.FromSuccess().ToArray());
      }

      public static void ErrorEquals<TValue>(string expected, IEither<TValue, ParseError> result)
      {
         IsError(result);
         Assert.AreEqual(expected, result.FromError().Message, "Error message");
      }
   }
}
