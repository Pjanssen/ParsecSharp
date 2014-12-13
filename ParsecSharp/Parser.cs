using PJanssen.ParsecSharp.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public abstract class Parser<T>
   {
      public abstract Either<T, ParseError> Parse(IInputReader input);
   }
}
