using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class StringParser : Parser<string>
   {
      private string str;
      private Parser<char> charParser;

      public StringParser(string str)
      {
         Throw.IfNull(str, "str");

         this.str = str;
         this.charParser = new AnyCharParser();
      }

      public override Either<string, ParseError> Parse(IInputReader input)
      {
         // TODO: optimize?
         for (int i = 0; i < str.Length; i++)
         {
            var result = this.charParser.Parse(input);
            if (result.IsError())
               return ParseResult.Error<string>(result.FromError());

            if (result.FromSuccess() != str[i])
               return ParseResult.Error<string>(input, "Unexpected '" + str[i] + "'");
         }

         return ParseResult.Success(this.str);
      }
   }
}
