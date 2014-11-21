using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class EitherTests
   {
      #region IsSuccess

      [TestMethod]
      public void IsSuccess_Success_ReturnsTrue()
      {
         Either<bool, int> either = Either.Success<bool, int>(true);
         
         Assert.IsTrue(either.IsSuccess());
      }

      [TestMethod]
      public void IsSuccess_Failure_ReturnsFalse()
      {
         Either<bool, int> either = Either.Fail<bool, int>(42);

         Assert.IsFalse(either.IsSuccess());
      }

      #endregion

      #region FromSuccess

      [TestMethod]
      public void FromSuccess_Success_ReturnsValue()
      {
         Either<bool, int> either = Either.Success<bool, int>(true);

         Assert.AreEqual(true, either.FromSuccess());
      }

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException))]
      public void FromSuccess_Failure_ThrowsException()
      {
         Either<bool, int> either = Either.Fail<bool, int>(42);

         either.FromSuccess();
      }

      #endregion

      #region IsFailure

      [TestMethod]
      public void IsFailure_Success_ReturnsFalse()
      {
         Either<bool, int> either = Either.Success<bool, int>(true);

         Assert.IsFalse(either.IsFailure());
      }

      [TestMethod]
      public void IsFailure_Failure_ReturnsTrue()
      {
         Either<bool, int> either = Either.Fail<bool, int>(42);

         Assert.IsTrue(either.IsFailure());
      }

      #endregion

      #region FromFailure

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException))]
      public void FromFailure_Success_ThrowsException()
      {
         Either<bool, int> either = Either.Success<bool, int>(true);

         either.FromFailure();
      }

      [TestMethod]
      public void FromFailure_Failure_ReturnsValue()
      {
         Either<bool, int> either = Either.Fail<bool, int>(42);

         Assert.AreEqual(42, either.FromFailure());
      }

      #endregion

      #region Select

      [TestMethod]
      public void Select_Success_ReturnsSelectedValue()
      {
         var result = from x in Either.Success<bool, int>(true)
                      select x.ToString();

         Assert.AreEqual("True", result.FromSuccess());
      }

      [TestMethod]
      public void Select_Failure_ReturnsFailure()
      {
         var result = from x in Either.Fail<bool, int>(42)
                      select x.ToString();

         Assert.AreEqual(42, result.FromFailure());
      }

      #endregion

      #region SelectMany

      [TestMethod]
      public void SelectMany_Successes_ReturnsSelectedValue()
      {
         var result = from x in Either.Success<string, bool>("x")
                      from y in Either.Success<string, bool>("y")
                      from z in Either.Success<string, bool>("z")
                      select x + y + z;

         Assert.AreEqual("xyz", result.FromSuccess());
      }

      [TestMethod]
      public void SelectMany_Failure_ReturnsFailure()
      {
         var result = from x in Either.Success<string, bool>("x")
                      from y in Either.Fail<string, bool>(true)
                      from z in Either.Success<string, bool>("z")
                      select x + y + z;

         Assert.AreEqual(true, result.FromFailure());
      }

      #endregion
   }
}
