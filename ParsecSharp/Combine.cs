﻿using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Combine
   {
      /// <summary>
      /// Applies parser A, followed by parser B. Returns the result of parser B.
      /// </summary>
      public static IParser<TValueB> Before<TValueA, TValueB>(this IParser<TValueA> parserA, IParser<TValueB> parserB)
      {
         return from a in parserA
                from b in parserB
                select b;
      }

      /// <summary>
      /// Applies the between parser followed by value and between, returning the result of the value parser.
      /// </summary>
      public static IParser<TValue> Between<TBetween, TValue>(this IParser<TValue> value, IParser<TBetween> between)
      {
         return Between(value, between, between);
      }

      /// <summary>
      /// Applies the open parser followed by value and close, returning the result of the value parser.
      /// </summary>
      public static IParser<TValue> Between<TOpen, TClose, TValue>(this IParser<TValue> value, IParser<TOpen> open, IParser<TClose> close)
      {
         return from o in open
                from v in value
                from c in close
                select v;
      }

      /// <summary>
      /// Tries to apply the given parsers in order, until one of them succeeds. 
      /// Returns the value of the succeeding parser.
      /// </summary>
      public static IParser<T> Choose<T>(params IParser<T>[] parsers)
      {
         return Choose((IEnumerable<IParser<T>>)parsers);
      }

      /// <summary>
      /// Tries to apply the given parsers in order, until one of them succeeds. 
      /// Returns the value of the succeeding parser.
      /// </summary>
      public static IParser<T> Choose<T>(IEnumerable<IParser<T>> parsers)
      {
         return parsers.Aggregate( Parse.Fail<T>("Empty choose sequence")
                                 , (acc, p) => acc.Or(p));
      }

      /// <summary>
      /// Applies the first parser and returns its value if it succeeds. 
      /// If it fails without consuming any input, the second parser is applied.
      /// </summary>
      public static IParser<T> Or<T>(this IParser<T> parserA, IParser<T> parserB)
      {
         return new OrParser<T>(parserA, parserB);
      }

      /// <summary>
      /// Applies the parser zero or more times.
      /// </summary>
      public static IParser<IEnumerable<T>> Many<T>(this IParser<T> parser)
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
      public static IParser<string> Many(this IParser<char> parser)
      {
         Func<StringBuilder> seed = () => new StringBuilder();
         Func<StringBuilder, char, StringBuilder> func = (builder, c) => builder.Append(c);
         Func<StringBuilder, string> sel = builder => builder.ToString();

         return parser.Aggregate(seed, func, sel);
      }

      /// <summary>
      /// Applies the parser one or more times.
      /// </summary>
      public static IParser<IEnumerable<T>> Many1<T>(this IParser<T> parser)
      {
         return from x in parser
                from xs in Many(parser)
                select new[] { x }.Concat(xs);
      }

      /// <summary>
      /// Applies the parser one or more times.
      /// </summary>
      public static IParser<string> Many1(this IParser<char> parser)
      {
         return from x in parser
                from xs in Many(parser)
                select x + xs;
      }

      /// <summary>
      /// Applies parser A, followed by parser B. Returns the result of parser A.
      /// </summary>
      public static IParser<TValueA> FollowedBy<TValueA, TValueB>(this IParser<TValueA> parserA, IParser<TValueB> parserB)
      {
         return from a in parserA
                from b in parserB
                select a;
      }

      /// <summary>
      /// Only succeeds when the second parser fails. Returns the result of the first parser.
      /// </summary>
      public static IParser<TValueA> NotFollowedBy<TValueA, TValueB>(this IParser<TValueA> parserA, IParser<TValueB> parserB)
      {
         return from a in parserA
                from b in Parse.Not(parserB)
                select a;
      }

      /// <summary>
      /// Parses zero or more occurrences of a parser, each followed by a separator parser.
      /// </summary>
      public static IParser<IEnumerable<T>> SeparatedBy<T, TSep>(this IParser<T> parser, IParser<TSep> separator)
      {
         return SeparatedBy1(parser, separator).Or(Parse.Succeed(Enumerable.Empty<T>()));
      }

      /// <summary>
      /// Parses one or more occurrences of a parser, each followed by a separator parser.
      /// </summary>
      public static IParser<IEnumerable<T>> SeparatedBy1<T, TSep>(this IParser<T> parser, IParser<TSep> separator)
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
