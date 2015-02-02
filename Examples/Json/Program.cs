using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PJanssen.ParsecSharp;
using System.IO;
using Json.Syntax;

namespace Json
{
   class Program
   {
      static void Main(string[] args)
      {
         var parser = Parser.JsonValue;
         using (FileStream stream = File.OpenRead(args[0]))
         {
            var result = parser.Parse(stream, Encoding.UTF8);
         }

         Console.Read();
      }
   }
}
