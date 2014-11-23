using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp.CharStream
{
   public class SimpleCharStream : StringCharStream
   {
      public SimpleCharStream(Stream input, Encoding encoding)
         : base(GetStringInput(input, encoding))
      {
      }

      private static string GetStringInput(Stream input, Encoding encoding)
      {
         using (StreamReader reader = new StreamReader(input, encoding))
         {
            return reader.ReadToEnd();
         }
      }
   }
}
