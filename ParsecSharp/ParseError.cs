using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public class ParseError
   {
      private Position position;
      private Func<string> getMessageFn;

      public ParseError(Position position, string message)
         : this(position, () => message)
      {
      }

      public ParseError(Position position, Func<string> getMessageFn)
         : this(position, getMessageFn, null)
      {
      }

      public ParseError(Position position, Func<string> getMessageFn, ParseError innerError)
      {
         Throw.IfNull(position, "position");
         Throw.IfNull(getMessageFn, "getMessageFn");

         this.position = position;
         this.getMessageFn = getMessageFn;
         this.InnerError = innerError;
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
         get
         {
            return this.getMessageFn();
         }
      }

      public ParseError InnerError
      {
         get;
         private set;
      }

      public override string ToString()
      {
         string innerErrorMsg = "";
         if (InnerError != null)
            innerErrorMsg = InnerError.Message + ". ";

         return string.Format("Parser error at line {0}, column {1}: \r\n{2}{3}."
                             , Line, Column, innerErrorMsg, Message);
      }
   }
}
