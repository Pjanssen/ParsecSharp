using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public class StreamInputReader : AbstractInputReader
   {
      private StreamReader reader;
      private Encoding encoding;

      public StreamInputReader(Stream stream, Encoding encoding)
      {
         Throw.IfNull(stream, "stream");
         Throw.IfNull(encoding, "Encoding");

         this.reader = new StreamReader(stream, encoding);
         this.encoding = encoding;
         this.offset = encoding.GetPreamble().Length;
      }

      override public char Read()
      {
         if (EndOfStream)
            return '\0';

         char c = (char)reader.Read();

         this.offset += this.encoding.GetByteCount(new[] { c });
         UpdatePosition(c);

         return c;
      }

      override public void Seek(Position position)
      {
         this.reader.DiscardBufferedData();
         this.reader.BaseStream.Seek(position.Offset, SeekOrigin.Begin);
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
