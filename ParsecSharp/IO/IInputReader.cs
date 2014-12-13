using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public interface IInputReader
   {
      /// <summary>
      /// Reads the next character from the input and advances the position by one character.
      /// </summary>
      char Read();

      /// <summary>
      /// Repositions the input reader to the given position.
      /// </summary>
      void Seek(Position position);

      /// <summary>
      /// Returns the current position in the reader.
      /// </summary>
      Position GetPosition();

      /// <summary>
      /// Gets a value that indicates whether the reader has reached the end of the input.
      /// </summary>
      bool EndOfStream
      {
         get;
      }
   }
}
