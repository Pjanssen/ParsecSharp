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
            
            return Either.Success<char, string>(input.Read());
         };
      }

      private static Parser<char> Satisfy(Predicate<char> predicate, Func<char, string> errorMessage)
      {
         Throw.IfNull(predicate, "predicate");

         return input =>
         {
            return Any()(input).Test(predicate, errorMessage);
         };
      }

      /// <summary>
      /// Succeeds for any character for which the given predicate returns true. Returns the parsed character.
      /// </summary>
      public static Parser<char> Satisfy(Predicate<char> predicate)
      {
         return Satisfy(predicate, c => "Unexpected '" + c + "'");
      }

      /// <summary>
      /// Succeeds for the given character. Returns the parsed character.
      /// </summary>
      public static Parser<char> Char(char character)
      {
         Func<char, string> error = c => string.Format("Expected '{0}', got '{1}'"
                                                      , character
                                                      , c);

         return Satisfy(c => c == character, error);
      }

      /// <summary>
      /// Succeeds if the current character is in the given sequence of characters. Returns the parsed character.
      /// </summary>
      public static Parser<char> OneOf(IEnumerable<char> characters)
      {
         Func<char, string> error = c => string.Format("Expected one of \"{0}\", got '{1}'"
                                                      , string.Concat(characters)
                                                      , c);

         return Satisfy(c => characters.Contains(c), error);
      }
   }
}
