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

      private static readonly IParser<char> ValueSeparator = 
            Chars.Char(_Separator);

      private static readonly IParser<char> LineSeparator =
            Chars.EndOfLine();

      public static readonly IParser<string> UnquotedValue = 
            Chars.NoneOf(_Separator, '\r', '\n').Many();

      private static readonly IParser<char> Quote = 
            Chars.Char('"');

      private static readonly IParser<char> EscapedQuote =
            Quote.FollowedBy(Quote).Try();

      public static readonly IParser<string> QuotedValue =
            (EscapedQuote.Or(Chars.Not('"'))).Many()
                                             .Between(Quote);

      public static readonly IParser<string> Value =
            QuotedValue.Or(UnquotedValue);


      public static readonly IParser<IEnumerable<string>> Line =
            Value.SeparatedBy(ValueSeparator);


      public static readonly IParser<IEnumerable<IEnumerable<string>>> Lines =
            Line.SeparatedBy(LineSeparator);
   }
}
