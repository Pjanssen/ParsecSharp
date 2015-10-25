using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PJanssen.ParsecSharp.IO;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class ChainParser<T> : IParser<T>
   {
      private IEnumerable<IParser<T>> parsers;

      public ChainParser(IEnumerable<IParser<T>> parsers)
      {
         Throw.IfNull(parsers, "parsers");

         this.parsers = parsers;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         IEither<T, ParseError> result = null;

         foreach (IParser<T> parser in parsers)
         {
            result = parser.Parse(input);

            if (result.IsError)
               break;
         }

         return result;
      }
   }
}
