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
      private PredicateParser<char> parser;

      public StringParser(string str)
      {
         Throw.IfNull(str, "str");

         this.str = str;
         this.parser = new PredicateParser<char>(new AnyCharParser(), x => true);
      }

      public override Either<string, ParseError> Parse(IInputReader input)
      {
         for (int i = 0; i < str.Length; i++)
         {
            this.parser.Predicate = str[i].Equals;

            var result = this.parser.Parse(input);
            if (result.IsError())
               return ParseResult.Error<string>(result.FromError());
         }

         return ParseResult.Success(this.str);
      }
   }
}
