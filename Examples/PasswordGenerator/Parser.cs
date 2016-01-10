using PasswordGenerator.Syntax;
using PJanssen.ParsecSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordGenerator
{
   public static class Parser
   {
      public static readonly IParser<IGenerator> GeneratorParser = from expressions in Expressions().FollowedByEof()
                                                                   select new CompositeGenerator(expressions);

      static IParser<IEnumerable<IGenerator>> Expressions()
      {
         return (QuantifiedExpression().Try().Or(Expression())).Many();
      }

      static IParser<IGenerator> Expression()
      {
         return Parse.Choose(AnyChar(), CharSet(), CharLiteral());
      }

      static IParser<IGenerator> AnyChar()
      {
         return from c in Chars.Char('.')
                select new AnyCharGenerator();
      }

      static IParser<IGenerator> CharSet()
      {
         return from open in Chars.Char('[')
                from cs in (CharRange().Try().Or(SingleChar())).Many1()
                from close in Chars.Char(']')
                select new CharSetGenerator(string.Concat(cs.ToArray()));
      }

      static IParser<string> SingleChar()
      {
         return from c in Chars.NoneOf("]")
                select c.ToString();
      }

      static IParser<string> CharRange()
      {
         return from cFrom in Chars.NoneOf("-]")
                from sep in Chars.Char('-')
                from cTo in Chars.NoneOf("-]")
                select CreateCharRange(cFrom, cTo);
      }

      private static string CreateCharRange(char cFrom, char cTo)
      {
         StringBuilder result = new StringBuilder();
         for (int i = cFrom; i < cTo; i++)
         {
            result.Append((char)i);
         }
         return result.ToString();
      }

      static IParser<IGenerator> CharLiteral()
      {
         return from c in Chars.Any()
                select (IGenerator)new CharLiteralGenerator(c);
      }


      static IParser<IGenerator> QuantifiedExpression()
      {
         return from e in Expression()
                from q in Quantifier()
                select (IGenerator)new QuantifiedExpression(e, q);
      }

      static IParser<IQuantifier> Quantifier()
      {
         return Parse.Choose(ZeroOrMore(), ZeroOrOne(), OneOrMore(), Range());
      }

      static IParser<IQuantifier> ZeroOrMore()
      {
         return from _ in Chars.Char('*')
                select new RangeQuantifier(0, 10);
      }

      static IParser<IQuantifier> OneOrMore()
      {
         return from _ in Chars.Char('+')
                select new RangeQuantifier(1, 10);
      }

      static IParser<IQuantifier> ZeroOrOne()
      {
         return from _ in Chars.Char('?')
                select new RangeQuantifier(0, 1);
      }

      static IParser<IQuantifier> Range()
      {
         return from open in Chars.Char('{')
                from min in Numeric.Int()
                from sep in Chars.Char(',')
                from max in Numeric.Int()
                from close in Chars.Char('}')
                where min > 0 && max >= min
                select new RangeQuantifier(min, max);
      }
   }
}
