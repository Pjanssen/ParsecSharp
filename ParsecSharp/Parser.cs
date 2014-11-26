using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PJanssen.ParsecSharp.IO;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// A monadic parser function that returns either Success with the parsed result,
   /// or Error with an error message.
   /// </summary>
   /// <typeparam name="TValue">The type of the resulting value.</typeparam>
   public delegate Either<TValue, ParserError> Parser<TValue>(ICharStream input);
}
