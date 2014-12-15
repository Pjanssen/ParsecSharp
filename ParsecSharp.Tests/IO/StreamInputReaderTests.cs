using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   [TestClass]
   public class StreamInputReaderTests
   {
      private IInputReader CreateInputStream(string input)
      {
         MemoryStream stream = new MemoryStream();
         StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
         writer.Write(input);
         writer.Flush();
         stream.Position = 0;

         return new StreamInputReader(stream, Encoding.UTF8);
      }

      #region Read

      [TestMethod]
      public void Read_EndOfString_ReturnsCharNull()
      {
         IInputReader stream = CreateInputStream("");

         char result = stream.Read();

         Assert.AreEqual('\0', result);
      }

      [TestMethod]
      public void Read_NewReader_ReturnsFirstChar()
      {
         IInputReader stream = CreateInputStream("abc");

         char result = (char)stream.Read();

         Assert.AreEqual('a', result);
      }

      [TestMethod]
      public void Read_ReadsCharAtPosition()
      {
         IInputReader stream = CreateInputStream("abc");

         stream.Read();
         char result = (char)stream.Read();

         Assert.AreEqual('b', result);
      }

      [TestMethod]
      public void Read_NonAsciiChars()
      {
         IInputReader stream = CreateInputStream("éすå");

         Assert.AreEqual('é', stream.Read());
         Assert.AreEqual('す', stream.Read());
         Assert.AreEqual('å', stream.Read());
      }

      #endregion

      #region GetPosition

      [TestMethod]
      public void GetPosition_NothingRead()
      {
         IInputReader stream = CreateInputStream("");

         Position position = stream.GetPosition();

         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(1, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_ReadChar()
      {
         IInputReader stream = CreateInputStream("abc\nxyz");
         stream.Read();

         Position position = stream.GetPosition();

         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(2, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_ReadNewLine()
      {
         IInputReader stream = CreateInputStream("abc\nxyz");
         stream.Read();
         stream.Read();
         stream.Read();
         stream.Read();

         Position position = stream.GetPosition();

         Assert.AreEqual(2, position.Line, "Line");
         Assert.AreEqual(1, position.Column, "Column");
      }

      [TestMethod]
      public void GetPosition_Read_DoesNotUpdateReturnedPosition()
      {
         IInputReader stream = CreateInputStream("abc");
         Position position = stream.GetPosition();

         stream.Read();

         Assert.AreEqual(1, position.Line, "Line");
         Assert.AreEqual(1, position.Column, "Column");
      }

      #endregion

      #region Seek

      [TestMethod]
      public void Seek_UpdatesPosition()
      {
         IInputReader stream = CreateInputStream("abc");
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
         IInputReader stream = CreateInputStream("a");

         stream.Read();

         Assert.IsTrue(stream.EndOfStream);
      }

      [TestMethod]
      public void EndOfStream_CharsLeftToRead_ReturnsFalse()
      {
         IInputReader stream = CreateInputStream("abc");

         stream.Read();

         Assert.IsFalse(stream.EndOfStream);
      }

      #endregion
   }
}
