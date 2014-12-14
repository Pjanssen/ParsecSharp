using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Combine
   {
      /// <summary>
      /// Applies the first parser and returns its value if it succeeds. 
      /// If it fails without consuming any input, the second parser is applied.
      /// </summary>
      public static Parser<T> Or<T>(this Parser<T> parserA, Parser<T> parserB)
      {
         return new OrParser<T>(parserA, parserB);
      }

      /// <summary>
      /// Applies the parser zero or more times.
      /// </summary>
      public static Parser<IEnumerable<T>> Many<T>(this Parser<T> parser)
      {
         Func<List<T>> seed = () => new List<T>();
         Func<List<T>, T, List<T>> func = (xs, x) =>
         {
            xs.Add(x);
            return xs;
         };
         Func<List<T>, IEnumerable<T>> sel = xs => (IEnumerable<T>)xs;

         return parser.Aggregate(seed, func, sel);
      }

      /// <summary>
      /// Applies the parser zero or more times.
      /// </summary>
      public static Parser<string> Many(this Parser<char> parser)
      {
         Func<StringBuilder> seed = () => new StringBuilder();
         Func<StringBuilder, char, StringBuilder> func = (builder, c) => builder.Append(c);
         Func<StringBuilder, string> sel = builder => builder.ToString();

         return parser.Aggregate(seed, func, sel);
      }

      /// <summary>
      /// Applies the parser one or more times.
      /// </summary>
      public static Parser<IEnumerable<T>> Many1<T>(this Parser<T> parser)
      {
         return from x in parser
                from xs in Many(parser)
                select new[] { x }.Concat(xs);
      }

      /// <summary>
      /// Applies the parser one or more times.
      /// </summary>
      public static Parser<string> Many1(this Parser<char> parser)
      {
         return from x in parser
                from xs in Many(parser)
                select x + xs;
      }
   }
}
