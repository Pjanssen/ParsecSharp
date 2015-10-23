using PasswordGenerator.Syntax;
using PJanssen.ParsecSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PasswordGenerator
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length != 1)
            WriteHelp();
         else
         {
            GeneratePassword(args[0]);
         }
      }

      private static void WriteHelp()
      {
         Console.WriteLine("Usage:");
         Console.WriteLine("pwdgen \"regex\"");
         Console.WriteLine("Example:");
         Console.WriteLine("pwdgen \"[0-9a-zA-Z]{6,12}\"");
      }

      private static void GeneratePassword(string pwdRegex)
      {
         IEither<IGenerator, ParseError> generator = Parser.GeneratorParser.Parse(pwdRegex);
         if (generator.IsError)
         {
            Console.WriteLine(generator.FromError());
         }
         else
         {
            string password = generator.FromSuccess().Generate(new Random());
            Console.WriteLine(password);
            ValidatePassword(pwdRegex, password);
         }
      }

      private static void ValidatePassword(string pwdRegex, string password)
      {
         Regex regex = new Regex(pwdRegex);
         Console.WriteLine("Password matches regex: " + regex.IsMatch(password).ToString());
      }
   }
}
