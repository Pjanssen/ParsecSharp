using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp.IO
{
   [TestClass]
   public class StringInputStreamTests
   {
      #region Read

      [TestMethod]
      public void Read_EndOfString_ReturnsCharNull()
      {
         IInputStream stream = new StringInputStream("");

         char result = stream.Read();

         Assert.AreEqual('\0', result);
      }

      [TestMethod]
      public void Read_NewReader_ReturnsFirstChar()
      {
         IInputStream stream = new StringInputStream("abc");

         char result = (char)stream.Read();

         Assert.AreEqual('a', result);
      }

      [TestMethod]
      public void Read_ReadsCharAtPosition()
      {
         IInputStream stream = new StringInputStream("abc");

         stream.Read();
         char result = (char)stream.Read();

         Assert.AreEqual('b', result);
      }

      [TestMethod]
      public void Read_NonAsciiChars()
      {
         IInputStream stream = new StringInputStream("éすå");

         Assert.AreEqual('é', stream.Read());
         Assert.AreEqual('す', stream.Read());
         Assert.AreEqual('å', stream.Read());
      }

      #endregion

      #region GetPosition

      [TestMethod]
      public void GetPosition_NothingRead()
      {
         IInputStream stream = new StringInputStream("");

         Position position = stream.GetPosition();

         Assert.AreEqual(0, position.Offset, "offset");
         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(1, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_ReadChar()
      {
         IInputStream stream = new StringInputStream("abc\nxyz");
         stream.Read();

         Position position = stream.GetPosition();

         Assert.AreEqual(1, position.Offset, "offset");
         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(2, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_ReadNewLine()
      {
         IInputStream stream = new StringInputStream("abc\nxyz");
         stream.Read();
         stream.Read();
         stream.Read();
         stream.Read();

         Position position = stream.GetPosition();

         Assert.AreEqual(4, position.Offset, "offset");
         Assert.AreEqual(2, position.Line, "Line");
         Assert.AreEqual(1, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_Read_DoesNotUpdateReturnedPosition()
      {
         IInputStream stream = new StringInputStream("abc");
         Position position = stream.GetPosition();

         stream.Read();

         Assert.AreEqual(0, position.Offset, "Offset");
         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(1, position.Column, "Column");
      }

      #endregion

      #region Seek

      [TestMethod]
      public void Seek_UpdatesPosition()
      {
         IInputStream stream = new StringInputStream("abc");
         Position position = stream.GetPosition();

         stream.Read();
         stream.Read();
         stream.Seek(position);

         Position newPosition = stream.GetPosition();
         Assert.AreEqual(position, newPosition);

         char result = (char)stream.Read();
         Assert.AreEqual('a', result);
      }

      #endregion

      #region EndOfStream

      [TestMethod]
      public void EndOfStream_NoCharsLeftToRead_ReturnsTrue()
      {
         IInputStream stream = new StringInputStream("a");

         stream.Read();

         Assert.IsTrue(stream.EndOfStream);
      }

      [TestMethod]
      public void EndOfStream_CharsLeftToRead_ReturnsFalse()
      {
         IInputStream stream = new StringInputStream("abc");

         stream.Read();

         Assert.IsFalse(stream.EndOfStream);
      }

      #endregion
   }
}
