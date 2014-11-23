using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp.CharStream
{
   [TestClass]
   public class StringReaderTests
   {
      #region Read

      [TestMethod]
      public void Read_EndOfString_ReturnsCharNull()
      {
         ICharStream stream = new StringCharStream("");

         char result = stream.Read();

         Assert.AreEqual('\0', result);
      }

      [TestMethod]
      public void Read_NewReader_ReturnsFirstChar()
      {
         ICharStream stream = new StringCharStream("abc");

         char result = (char)stream.Read();

         Assert.AreEqual('a', result);
      }

      [TestMethod]
      public void Read_AdvancesPosition()
      {
         ICharStream stream = new StringCharStream("abc");

         stream.Read();

         Assert.AreEqual(1, stream.Position);
      }

      [TestMethod]
      public void Read_ReadsCharAtPosition()
      {
         ICharStream stream = new StringCharStream("abc");

         stream.Read();
         char result = (char)stream.Read();

         Assert.AreEqual('b', result);
      }

      [TestMethod]
      public void Read_NonAsciiChars()
      {
         ICharStream stream = new StringCharStream("éすå");

         Assert.AreEqual('é', stream.Read());
         Assert.AreEqual('す', stream.Read());
         Assert.AreEqual('å', stream.Read());
      }

      #endregion

      #region Seek

      [TestMethod]
      public void Seek_SetsPosition()
      {
         ICharStream stream = new StringCharStream("abc");

         stream.Seek(1);
         Assert.AreEqual(1, stream.Position);

         char result = (char)stream.Read();
         Assert.AreEqual('b', result);
      }

      [TestMethod]
      public void Seek_NegativePosition_SetPositionToZero()
      {
         ICharStream stream = new StringCharStream("abc");

         stream.Seek(-1);

         Assert.AreEqual(0, stream.Position);
      }

      [TestMethod]
      public void Seek_PositionLargerThanLength_SetPositionToLength()
      {
         ICharStream stream = new StringCharStream("abc");

         stream.Seek(4);

         Assert.AreEqual(3, stream.Position);
      }


      [TestMethod]
      public void Seek_NonAsciiChars()
      {
         ICharStream stream = new StringCharStream("éすå");

         stream.Seek(2);
         Assert.AreEqual('å', stream.Read());
         stream.Seek(1);
         Assert.AreEqual('す', stream.Read());
         stream.Seek(0);
         Assert.AreEqual('é', stream.Read());
      }

      #endregion

      #region EndOfStream

      [TestMethod]
      public void EndOfStream_NoCharsLeftToRead_ReturnsTrue()
      {
         ICharStream stream = new StringCharStream("a");

         stream.Read();

         Assert.IsTrue(stream.EndOfStream);
      }

      [TestMethod]
      public void EndOfStream_CharsLeftToRead_ReturnsFalse()
      {
         ICharStream stream = new StringCharStream("abc");

         stream.Read();

         Assert.IsFalse(stream.EndOfStream);
      }

      #endregion
   }
}
