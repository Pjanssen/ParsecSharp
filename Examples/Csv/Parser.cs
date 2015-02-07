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
      private const char _CR = '\r';
      private const char _LF = '\n';

      private static readonly Parser<char> ValueSeparator =
            Chars.Char(_Separator);

      private static readonly Parser<char> LineSeparator =
            Chars.EndOfLine();

      public static readonly Parser<string> Value = 
            Chars.NoneOf(_Separator, _CR, _LF).Many();


      public static readonly Parser<IEnumerable<string>> Line =
            Value.SeparatedBy(ValueSeparator);


      public static readonly Parser<IEnumerable<IEnumerable<string>>> Lines =
            Line.SeparatedBy(LineSeparator);
   }
}
