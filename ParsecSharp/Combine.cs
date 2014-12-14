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
      /// Applies the open parser followed by value and close, returning the result of the value parser.
      /// </summary>
      public static Parser<TValue> Between<TOpen, TClose, TValue>(this Parser<TValue> value, Parser<TOpen> open, Parser<TClose> close)
      {
         return from o in open
                from v in value
                from c in close
                select v;
      }

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

      /// <summary>
      /// Only succeeds when the second parser fails. Returns the result of the first parser.
      /// </summary>
      public static Parser<TValueA> NotFollowedBy<TValueA, TValueB>(this Parser<TValueA> parserA, Parser<TValueB> parserB)
      {
         return from a in parserA
                from b in Parse.Not(parserB)
                select a;
      }

      /// <summary>
      /// Parses zero or more occurrences of a parser, each followed by a separator parser.
      /// </summary>
      public static Parser<IEnumerable<T>> SeparatedBy<T, TSep>(this Parser<T> parser, Parser<TSep> separator)
      {
         return SeparatedBy1(parser, separator) | Parse.Succeed(Enumerable.Empty<T>());
      }

      /// <summary>
      /// Parses one or more occurrences of a parser, each followed by a separator parser.
      /// </summary>
      public static Parser<IEnumerable<T>> SeparatedBy1<T, TSep>(this Parser<T> parser, Parser<TSep> separator)
      {
         return from x in parser
                from xs in
                   Many(from _ in separator
                        from rest in parser
                        select rest)
                select new[] { x }.Concat(xs);
      }
   }
}
