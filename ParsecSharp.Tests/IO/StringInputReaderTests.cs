using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp.IO
{
   [TestClass]
   public class StringInputReaderTests
   {
      #region Read

      [TestMethod]
      public void Read_EndOfString_ReturnsCharNull()
      {
         IInputReader stream = new StringInputReader("");

         char result = stream.Read();

         Assert.AreEqual('\0', result);
      }

      [TestMethod]
      public void Read_NewReader_ReturnsFirstChar()
      {
         IInputReader stream = new StringInputReader("abc");

         char result = (char)stream.Read();

         Assert.AreEqual('a', result);
      }

      [TestMethod]
      public void Read_ReadsCharAtPosition()
      {
         IInputReader stream = new StringInputReader("abc");

         stream.Read();
         char result = (char)stream.Read();

         Assert.AreEqual('b', result);
      }

      [TestMethod]
      public void Read_NonAsciiChars()
      {
         IInputReader stream = new StringInputReader("éすå");

         Assert.AreEqual('é', stream.Read());
         Assert.AreEqual('す', stream.Read());
         Assert.AreEqual('å', stream.Read());
      }

      #endregion

      #region GetPosition

      [TestMethod]
      public void GetPosition_NothingRead()
      {
         IInputReader stream = new StringInputReader("");

         Position position = stream.GetPosition();

         Assert.AreEqual(0, position.Offset, "offset");
         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(1, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_ReadChar()
      {
         IInputReader stream = new StringInputReader("abc\nxyz");
         stream.Read();

         Position position = stream.GetPosition();

         Assert.AreEqual(1, position.Offset, "offset");
         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(2, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_ReadNewLine()
      {
         IInputReader stream = new StringInputReader("abc\nxyz");
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
         IInputReader stream = new StringInputReader("abc");
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
         IInputReader stream = new StringInputReader("abc");
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
         IInputReader stream = new StringInputReader("a");

         stream.Read();

         Assert.IsTrue(stream.EndOfStream);
      }

      [TestMethod]
      public void EndOfStream_CharsLeftToRead_ReturnsFalse()
      {
         IInputReader stream = new StringInputReader("abc");

         stream.Read();

         Assert.IsFalse(stream.EndOfStream);
      }

      #endregion
   }
}
