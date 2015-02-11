using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class LabeledParser<T> : Parser<T>
   {
      private Parser<T> parser;
      private Func<string> msgFunc;

      public LabeledParser(Parser<T> parser, Func<string> msgFunc)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(msgFunc, "msgFunc");

         this.parser = parser;
         this.msgFunc = msgFunc;
      }

      public override Either<T, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsSuccess())
            return result;

         return ParseResult.Error<T>(input, msgFunc, result.FromError());
      }
   }
}
