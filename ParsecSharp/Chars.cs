using PJanssen.ParsecSharp.Parsers;
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
         return new AnyCharParser();
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
                  .Label(() => "Expected \"" + character + "\"");
      }

      /// <summary>
      /// Succeeds if the current character is in the given sequence of characters. Returns the parsed character.
      /// </summary>
      public static Parser<char> OneOf(IEnumerable<char> characters)
      {
         return Satisfy(characters.Contains)
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

      /// <summary>
      /// Succeeds if the current character is any except the given character. Returns the parsed character.
      /// </summary>
      public static Parser<char> Not(char character)
      {
         return Satisfy(c => c != character)
                  .Label(() => string.Format("Expected any char except '{0}'", character));
      }

      /// <summary>
      /// Succeeds if the current character is a letter. Returns the parsed character.
      /// </summary>
      public static Parser<char> Letter()
      {
         return Satisfy(char.IsLetter)
                  .Label(() => "Expected a letter");
      }

      /// <summary>
      /// Succeeds if the current character is a letter. Returns the parsed character.
      /// </summary>
      public static Parser<char> Digit()
      {
         return Satisfy(char.IsDigit)
                  .Label(() => "Expected a digit");
      }

      /// <summary>
      /// Succeeds if the current character is a letter or digit. Returns the parsed character.
      /// </summary>
      public static Parser<char> AlphaNum()
      {
         return Satisfy(char.IsLetterOrDigit)
                  .Label(() => "Expected a letter or digit");
      }

      /// <summary>
      /// Succeeds if the current character is a space. Returns the parsed character.
      /// </summary>
      public static Parser<char> Space()
      {
         return Satisfy(c => c == ' ')
                  .Label(() => "Expected a space");
      }

      /// <summary>
      /// Succeeds if the current character is a tab. Returns the parsed character.
      /// </summary>
      public static Parser<char> Tab()
      {
         return Satisfy(c => c == '\t')
                  .Label(() => "Expected a tab");
      }

      /// <summary>
      /// Succeeds if the current character is a whitespace character. Returns the parsed character.
      /// </summary>
      public static Parser<char> WhiteSpace()
      {
         return Satisfy(char.IsWhiteSpace)
                  .Label(() => "Expected a whitespace character");
      }

      /// <summary>
      /// Succeeds if the current character is a CR ('\r'), followed by an LF ('\n').
      /// Returns the line feed.
      /// </summary>
      public static Parser<char> CrLf()
      {
         return (from cr in Char('\r')
                 from lf in Char('\n')
                 select lf).Label(() => "Expected CRLF");
      }

      /// <summary>
      /// Succeeds if the current character is a LF ('\n') or CRLF ('\r\n').
      /// Returns the line feed.
      /// </summary>
      public static Parser<char> EndOfLine()
      {
         return Char('\n') | CrLf();
      }

      /// <summary>
      /// Succeeds if the current input matches the entire string. Returns the parsed string.
      /// </summary>
      public static Parser<string> String(string str)
      {
         return new StringParser(str)
                     .Label(() => "Expected \"" + str + "\"");
      }
   }
}
