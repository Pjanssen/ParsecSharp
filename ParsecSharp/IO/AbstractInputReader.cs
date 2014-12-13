using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public abstract class AbstractInputReader : IInputReader
   {
      protected int offset;
      protected int line;
      protected int column;

      protected AbstractInputReader()
      {
         this.offset = 0;
         this.line = 1;
         this.column = 1;
      }

      public abstract char Read();
      public abstract void Seek(Position position);
      public abstract bool EndOfStream
      {
         get;
      }

      public virtual Position GetPosition()
      {
         return new Position(this.offset, this.line, this.column);
      }

      protected void UpdatePosition(char c)
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

      protected void UpdatePosition(Position position)
      {
         this.offset = position.Offset;
         this.line = position.Line;
         this.column = position.Column;
      }
   }
}
