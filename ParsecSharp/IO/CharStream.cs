using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.IO
{
   public static class CharStream
   {
      /// <summary>
      /// Creates an ICharStream for the given string.
      /// </summary>
      public static ICharStream Create(string str)
      {
         return new StringCharStream(str);
      }

      /// <summary>
      /// Creates an ICharStream for the given stream.
      /// </summary>
      public static ICharStream Create(Stream stream, Encoding encoding)
      {
         return new SimpleCharStream(stream, encoding);
      }
   }
}
