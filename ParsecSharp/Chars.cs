using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   /// <summary>
   /// Defines commonly used character parsers.
   /// </summary>
   public static class Chars
   {
      /// <summary>
      /// Succeeds for any character. Returns the parsed character.
      /// </summary>
      public static Parser<char> Any()
      {
         return input =>
         {
            if (input.EndOfStream)
               return Either.Error<char, string>("Unexpected end of input");

            char c = input.Read();
            return Either.Success<char, string>(c);
         };
      }

      /// <summary>
      /// Succeeds for any character for which the given predicate returns true. Returns the parsed character.
      /// </summary>
      public static Parser<char> Satisfy(Predicate<char> predicate)
      {
         Throw.IfNull(predicate, "predicate");

         return Parse.Try(from c in Any()
                          where predicate(c)
                          select c);
      }

      /// <summary>
      /// Succeeds for the given character. Returns the parsed character.
      /// </summary>
      public static Parser<char> Char(char character)
      {
         return Satisfy(c => c == character)
                  .Label("Expected " + character);
      }

      /// <summary>
      /// Succeeds if the current character is in the given sequence of characters. Returns the parsed character.
      /// </summary>
      public static Parser<char> OneOf(IEnumerable<char> characters)
      {
         return Satisfy(c => characters.Contains(c))
                  .Label(() => string.Format("Expected one of \"{0}\"", string.Concat(characters)));
      }

      /// <summary>
      /// Succeeds if the current character is not in the given sequence of characters. Returns the parsed character.
      /// </summary>
      public static Parser<char> NoneOf(IEnumerable<char> characters)
      {
         return Satisfy(c => !characters.Contains(c))
                  .Label(() => string.Format("Expected any char except \"{0}\"", string.Concat(characters)));
      }

      public static Parser<string> String(string str)
      {
         return input =>
         {
            for (int i = 0; i < str.Length; i++)
            {
               var result = Char(str[i])(input);
               if (result.IsError())
                  return Either.Error<string, string>(result.FromError());
            }

            return Either.Success<string, string>(str);
         };
      }
   }
}
