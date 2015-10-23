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
         IEither<bool, int> either = Either.Success<bool, int>(true);
         
         Assert.IsTrue(either.IsSuccess);
      }

      [TestMethod]
      public void IsSuccess_Error_ReturnsFalse()
      {
         IEither<bool, int> either = Either.Error<bool, int>(42);

         Assert.IsFalse(either.IsSuccess);
      }

      #endregion

      #region FromSuccess

      [TestMethod]
      public void FromSuccess_Success_ReturnsValue()
      {
         IEither<bool, int> either = Either.Success<bool, int>(true);

         Assert.AreEqual(true, either.FromSuccess());
      }

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException))]
      public void FromSuccess_Error_ThrowsException()
      {
         IEither<bool, int> either = Either.Error<bool, int>(42);

         either.FromSuccess();
      }

      #endregion

      #region IsError

      [TestMethod]
      public void IsError_Success_ReturnsFalse()
      {
         IEither<bool, int> either = Either.Success<bool, int>(true);

         Assert.IsFalse(either.IsError);
      }

      [TestMethod]
      public void IsError_Error_ReturnsTrue()
      {
         IEither<bool, int> either = Either.Error<bool, int>(42);

         Assert.IsTrue(either.IsError);
      }

      #endregion

      #region FromError

      [TestMethod]
      [ExpectedException(typeof(InvalidOperationException))]
      public void FromError_Success_ThrowsException()
      {
         IEither<bool, int> either = Either.Success<bool, int>(true);

         either.FromError();
      }

      [TestMethod]
      public void FromError_Error_ReturnsValue()
      {
         IEither<bool, int> either = Either.Error<bool, int>(42);

         Assert.AreEqual(42, either.FromError());
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
      public void Select_Error_ReturnsError()
      {
         var result = from x in Either.Error<bool, int>(42)
                      select x.ToString();

         Assert.AreEqual(42, result.FromError());
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
      public void SelectMany_Error_ReturnsError()
      {
         var result = from x in Either.Success<string, bool>("x")
                      from y in Either.Error<string, bool>(true)
                      from z in Either.Success<string, bool>("z")
                      select x + y + z;

         Assert.AreEqual(true, result.FromError());
      }

      #endregion
   }
}
