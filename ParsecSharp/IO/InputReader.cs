using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public static class InputReader
   {
      /// <summary>
      /// Creates an IInputReader for the given string.
      /// </summary>
      public static IInputReader Create(string str)
      {
         return new StringInputReader(str);
      }

      /// <summary>
      /// Creates an IInputReader for the given stream.
      /// </summary>
      public static IInputReader Create(Stream stream, Encoding encoding)
      {
         return new StreamInputReader(stream, encoding);
      }
   }
}
