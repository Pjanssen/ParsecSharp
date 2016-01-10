using PJanssen.ParsecSharp.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Parse
   {
      /// <summary>
      /// Creates a parser that always succeeds and returns the given value without consuming input.
      /// </summary>
      public static IParser<T> Succeed<T>(T value)
      {
         return new SuccessParser<T>(value);
      }

      /// <summary>
      /// Creates a parser that always fails with the given message, without consuming input.
      /// </summary>
      public static IParser<T> Fail<T>(string message)
      {
         return new ErrorParser<T>(message);
      }

      /// <summary>
      /// Tries to parse the input using the given parser, only consuming input when the parser succeeds.
      /// </summary>
      public static IParser<T> Try<T>(this IParser<T> parser)
      {
         return new TryParser<T>(parser);
      }

      /// <summary>
      /// Only succeeds when the given parser fails. Returns a Unit.
      /// </summary>
      public static IParser<Unit> Not<T>(IParser<T> parser)
      {
         return new NegatedParser<T>(parser);
      }

      /// <summary>
      /// Succeeds only at the end of the input. Returns the default value of T.
      /// </summary>
      public static IParser<T> Eof<T>()
      {
         return new EofParser<T>();
      }

      /// <summary>
      /// Tries to apply the given parsers in order, until one of them succeeds. 
      /// Returns the value of the succeeding parser.
      /// </summary>
      public static IParser<T> Choose<T>(IEnumerable<IParser<T>> parsers)
      {
         return parsers.Aggregate(Parse.Fail<T>("Empty choose sequence")
                                 , (acc, p) => acc.Or(p));
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
      /// Applies the given parsers in order and returns the result of the last one.
      /// Only succeeds if all parsers succeed.
      /// </summary>
      public static IParser<T> Chain<T>(IEnumerable<IParser<T>> parsers)
      {
         return new ChainParser<T>(parsers);
      }

      /// <summary>
      /// Applies the given parsers in order and returns the result of the last one.
      /// Only succeeds if all parsers succeed.
      /// </summary>
      public static IParser<T> Chain<T>(params IParser<T>[] parsers)
      {
         return new ChainParser<T>(parsers);
      }

      /// <summary>
      /// Applies the given parsers in order and returns the result of each parser. 
      /// Only succeeds if all parsers succeed.
      /// </summary>
      public static IParser<IEnumerable<T>> Sequence<T>(IEnumerable<IParser<T>> parsers)
      {
         return new SequenceParser<T>(parsers);
      }

      /// <summary>
      /// Applies the given parsers in order and returns the result of each parser. 
      /// Only succeeds if all parsers succeed.
      /// </summary>
      public static IParser<IEnumerable<T>> Sequence<T>(params IParser<T>[] parsers)
      {
         return new SequenceParser<T>(parsers);
      }
   }
}
