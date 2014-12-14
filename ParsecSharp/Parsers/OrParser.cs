using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.Parsers
{
   internal class OrParser<T> : Parser<T>
   {
      private Parser<T> parserA;
      private Parser<T> parserB;

      public OrParser(Parser<T> parserA, Parser<T> parserB)
      {
         Throw.IfNull(parserA, "parserA");
         Throw.IfNull(parserB, "parserB");

         this.parserA = parserA;
         this.parserB = parserB;
      }

      public override Either<T, ParseError> Parse(IInputReader input)
      {
         Position position = input.GetPosition();

         var result = this.parserA.Parse(input);
         if (result.IsSuccess() || input.GetPosition() != position)
            return result;

         return this.parserB.Parse(input);

      }
   }
}
