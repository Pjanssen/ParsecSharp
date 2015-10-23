using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class StringParser : IParser<string>
   {
      private string str;
      private IParser<char> parser;
      private PredicateParser<char> predicateParser;

      public StringParser(string str)
      {
         Throw.IfNull(str, "str");

         this.str = str;
         this.predicateParser = new PredicateParser<char>(new AnyCharParser(), x => true);
         this.parser = new TryParser<char>(this.predicateParser);
      }

      public IEither<string, ParseError> Parse(IInputReader input)
      {
         for (int i = 0; i < str.Length; i++)
         {
            this.predicateParser.Predicate = str[i].Equals;

            var result = this.parser.Parse(input);
            if (result.IsError)
               return ParseResult.Error<string>(result.FromError());
         }

         return ParseResult.Success(this.str);
      }
   }
}
