using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public interface ICharStream
   {
      /// <summary>
      /// Reads the next character from the reader and advances the position by one character.
      /// </summary>
      char Read();

      /// <summary>
      /// Sets the position of the reader to the given offset, relative to the beginning of the input.
      /// </summary>
      void Seek(int position);

      /// <summary>
      /// Gets the current position in the stream, i.e. the total number of characters that have been read.
      /// </summary>
      int Position
      {
         get;
      }

      /// <summary>
      /// Gets a value that indicates whether the reader has reached the end of the stream.
      /// </summary>
      bool EndOfStream
      {
         get;
      }
   }
}
