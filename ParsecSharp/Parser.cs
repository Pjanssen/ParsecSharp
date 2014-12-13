using PJanssen.ParsecSharp.IO;
using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public abstract class Parser<T>
   {
      public abstract Either<T, ParseError> Parse(IInputReader input);

      /// <summary>
      /// Applies a projection function to the result of a parser.
      /// </summary>
      public Parser<TTarget> Select<TTarget>(Func<T, TTarget> func)
      {
         return new SelectParser<T, TTarget>(this, func);
      }
   }
}
