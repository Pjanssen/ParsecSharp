using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public interface IParser<out T>
   {
      /// <summary>
      /// Runs the parser on the given input.
      /// </summary>
      IEither<T, ParseError> Parse(IInputReader input);
   }
}
