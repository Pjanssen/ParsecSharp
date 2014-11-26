using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Combine
   {
      /// <summary>
      /// Only succeeds when the given parser fails.
      /// </summary>
      public static Parser<Unit> NotFollowedBy<TValue>(Parser<TValue> parser)
      {
         var unexpectedParser = from x in Parse.Try(parser)
                                from e in Error.UnexpectedParser<Unit>(x)
                                select e;

         return Combine.Or(unexpectedParser, Parse.Success(Unit.Instance));
      }

      /// <summary>
      /// Only succeeds when the second parser fails. Returns the result of the first parser.
      /// </summary>
      public static Parser<TValueA> NotFollowedBy<TValueA, TValueB>(this Parser<TValueA> parserA, Parser<TValueB> parserB)
      {
         return from a in parserA
                from b in NotFollowedBy(parserB)
                select a;
      }

      /// <summary>
      /// Applies the first parser and returns its value if it succeeds. 
      /// If it fails without consuming any input, the second parser is applied.
      /// </summary>
      public static Parser<TValue> Or<TValue>(this Parser<TValue> parserA, Parser<TValue> parserB)
      {
         return input =>
         {
            int position = input.Position;

            var result = parserA(input);
            if (result.IsSuccess() || input.Position != position)
               return result;

            return parserB(input);
         };
      }

      /// <summary>
      /// Tries to apply the given parsers in order, until one of them succeeds. 
      /// Returns the value of the succeeding parser.
      /// </summary>
      public static Parser<TValue> Choose<TValue>(params Parser<TValue>[] parsers)
      {
         return Choose((IEnumerable<Parser<TValue>>)parsers);
      }

      /// <summary>
      /// Tries to apply the given parsers in order, until one of them succeeds. 
      /// Returns the value of the succeeding parser.
      /// </summary>
      public static Parser<TValue> Choose<TValue>(IEnumerable<Parser<TValue>> parsers)
      {
         return parsers.Aggregate(Error.Fail<TValue>("Empty choose sequence")
                                 , (acc, p) => Combine.Or(acc, p));
      }

      /// <summary>
      /// Applies the parser zero or more times.
      /// </summary>
      public static Parser<IEnumerable<TValue>> Many<TValue>(this Parser<TValue> parser)
      {
         List<TValue> seed = new List<TValue>();
         Func<List<TValue>, TValue, List<TValue>> func = (xs, x) =>
         {
            xs.Add(x);
            return xs;
         };
         Func<List<TValue>, IEnumerable<TValue>> sel = xs => (IEnumerable<TValue>)xs;

         return parser.Aggregate(seed, func, sel);
      }

      /// <summary>
      /// Applies the parser zero or more times.
      /// </summary>
      public static Parser<string> Many(this Parser<char> parser)
      {
         StringBuilder seed = new StringBuilder();
         Func<StringBuilder, char, StringBuilder> func = (builder, c) => builder.Append(c);
         Func<StringBuilder, string> sel = builder => builder.ToString();

         return parser.Aggregate(seed, func, sel);
      }

      /// <summary>
      /// Applies the parser one or more times.
      /// </summary>
      public static Parser<IEnumerable<TValue>> Many1<TValue>(this Parser<TValue> parser)
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
      /// Applies the open parser followed by value and close, returning the result of the value parser.
      /// </summary>
      public static Parser<TValue> Between<TOpen, TClose, TValue>(this Parser<TValue> value, Parser<TOpen> open, Parser<TClose> close)
      {
         return from o in open
                from v in value
                from c in close
                select v;
      }

      public static Parser<IEnumerable<T>> SeparatedBy<T, TSep>(this Parser<T> parser, Parser<TSep> separator)
      {
         return SeparatedBy1(parser, separator)
                   .Or(Parse.Success(Enumerable.Empty<T>()));
      }

      public static Parser<IEnumerable<T>> SeparatedBy1<T, TSep>(this Parser<T> parser, Parser<TSep> separator)
      {
         return from x in parser
                from xs in Many(from _ in separator
                                from rest in parser
                                select rest)
                select new[] { x }.Concat(xs);
      }
   }
}
