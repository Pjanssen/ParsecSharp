using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.CharStream
{
   public class StringCharStream : ICharStream
   {
      private String input;
      private int length;
      private int position;

      public StringCharStream(String input)
      {
         Throw.IfNull(input, "input");
         
         this.input = input;
         this.length = input.Length;
         this.position = 0;
      }

      public char Read()
      {
         if (EndOfStream)
            return '\0';

         return input[this.position++];
      }

      public void Seek(int position)
      {
         this.position = Math.Min(this.length, Math.Max(0, position));
      }

      public int Position
      {
         get
         {
            return this.position;
         }
      }

      public bool EndOfStream
      {
         get { return this.position == this.length; }
      }
   }
}
