using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PJanssen.ParsecSharp;

namespace Json
{
   class Program
   {
      static void Main(string[] args)
      {
         var parser = Parser.Create();
         var result = parser.Parse("true");
      }
   }
}
