using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.old
{
   public class ParseError
   {
      private Position position;

      public ParseError(Position position, string message)
      {
         this.position = position;
         this.Message = message;
      }

      public int Line
      {
         get
         {
            return this.position.Line;
         }
      }

      public int Column
      {
         get
         {
            return this.position.Column;
         }
      }

      public string Message
      {
         get;
         private set;
      }

      public override string ToString()
      {
         return string.Format("Parser error at line {0}, column {1}: \r\n{2}"
                             , Line, Column, Message);
      }
   }
}
