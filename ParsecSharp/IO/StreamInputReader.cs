using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public class StreamInputReader : IInputReader
   {
      private Stream stream;
      private StreamReader reader;
      private int offset;
      private int line;
      private int column;


      public StreamInputReader(Stream stream, Encoding encoding)
      {
         Throw.IfNull(stream, "stream");

         this.stream = stream;
         this.reader = new StreamReader(stream, encoding);
         this.offset = 0;
         this.line = 1;
         this.column = 1;
      }

      public char Read()
      {
         if (EndOfStream)
            return '\0';

         char c = (char)reader.Read();

         this.offset++;
         SetLineAndColumn(c);

         return c;
      }

      private void SetLineAndColumn(char c)
      {
         if (c == '\n')
         {
            line++;
            column = 1;
         }
         else
         {
            column++;
         }
      }

      public void Seek(Position position)
      {
         this.reader.DiscardBufferedData();
         this.stream.Seek(position.Offset, SeekOrigin.Begin);
         this.offset = position.Offset;
         this.line = position.Line;
         this.column = position.Column;
      }

      public Position GetPosition()
      {
         return new Position(this.offset, this.line, this.column);
      }

      public bool EndOfStream
      {
         get
         {
            return reader.EndOfStream;
         }
      }
   }
}
