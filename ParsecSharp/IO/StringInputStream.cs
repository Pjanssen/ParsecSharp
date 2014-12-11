using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public class StringInputStream : IInputStream
   {
      private String input;
      private int length;
      private int offset;
      private int line;
      private int column;

      public StringInputStream(String input)
      {
         Throw.IfNull(input, "input");
         
         this.input = input;
         this.length = input.Length;
         this.offset = 0;
         this.line = 1;
         this.column = 1;
      }

      public char Read()
      {
         if (EndOfStream)
            return '\0';

         char c = input[this.offset++];

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

      public Position GetPosition()
      {
         return new Position(this.offset, this.line, this.column);
      }

      public void Seek(Position position)
      {
         Throw.IfNull(position, "position");

         this.offset = position.Offset;
         this.line = position.Line;
         this.column = position.Column;
      }

      public bool EndOfStream
      {
         get { return this.offset == this.length; }
      }
   }
}
