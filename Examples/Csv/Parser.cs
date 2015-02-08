using PJanssen.ParsecSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Csv
{
   public static class Parser
   {
      private const char _Separator = ',';

      private static readonly Parser<char> ValueSeparator = 
            Chars.Char(_Separator);

      private static readonly Parser<char> LineSeparator =
            Chars.EndOfLine();

      public static readonly Parser<string> UnquotedValue = 
            Chars.NoneOf(_Separator, '\r', '\n').Many();

      private static readonly Parser<char> Quote = 
            Chars.Char('"');

      private static readonly Parser<char> EscapedQuote =
            Parse.Try(Quote.FollowedBy(Quote));

      public static readonly Parser<string> QuotedValue =
            (EscapedQuote | Chars.Not('"')).Many()
                                           .Between(Quote);

      public static readonly Parser<string> Value =
            QuotedValue | UnquotedValue;


      public static readonly Parser<IEnumerable<string>> Line =
            Value.SeparatedBy(ValueSeparator);


      public static readonly Parser<IEnumerable<IEnumerable<string>>> Lines =
            Line.SeparatedBy(LineSeparator);
   }
}
