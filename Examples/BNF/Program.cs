using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PJanssen.ParsecSharp;
using System.IO;
using BNF.Syntax;

namespace BNF
{
   class Program
   {
      static void Main(string[] args)
      {
         var parser = Parser.Create();
         using (FileStream stream = File.OpenRead(args[0]))
         {
            var result = parser.Parse(stream, Encoding.UTF8);
            if (result.IsSuccess)
               WriteResult(result.FromSuccess());
            else
               WriteError(result.FromError());
         }

         Console.Read();
      }

      private static void WriteResult(IEnumerable<Rule> rules)
      {
         Console.WriteLine("Rules read:");
         foreach (Rule rule in rules)
         {
            Console.WriteLine(rule);
         }
      }

      private static void WriteError(ParseError parseError)
      {
         Console.WriteLine(parseError);
      }
   }
}
