using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public class StreamInputReader : AbstractInputReader
   {
      private Stream stream;
      private StreamReader reader;

      public StreamInputReader(Stream stream, Encoding encoding)
      {
         Throw.IfNull(stream, "stream");

         this.stream = stream;
         this.reader = new StreamReader(stream, encoding);
      }

      override public char Read()
      {
         if (EndOfStream)
            return '\0';

         char c = (char)reader.Read();

         this.offset++;
         UpdatePosition(c);

         return c;
      }

      override public void Seek(Position position)
      {
         this.reader.DiscardBufferedData();
         this.stream.Seek(position.Offset, SeekOrigin.Begin);
         UpdatePosition(position);
      }

      override public bool EndOfStream
      {
         get
         {
            return reader.EndOfStream;
         }
      }
   }
}
