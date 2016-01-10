using BNF.Syntax;
using PJanssen.ParsecSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNF
{
   public static class Parser
   {
      public static IParser<IEnumerable<Rule>> Create()
      {
         return Rule.Many()
                    .FollowedByEof();
      }

      private static IParser<string> SymbolName =
            Chars.Satisfy(c => Char.IsLetterOrDigit(c) || c == '-')
                 .Many1();

      private static IParser<NonTerminal> Identifier =
            from open in Chars.Char('<')
            from symbol in SymbolName
            from close in Chars.Char('>')
            select new Identifier(symbol);

      private static IParser<string> Assignment = Chars.String("::=");

      private static IParser<NonTerminal> Terminal =
            from open in Chars.OneOf("\"'")
            from value in Chars.Not(open).Many()
            from close in Chars.Char(open)
            select new Terminal(value);

      private static IParser<NonTerminal> Concatenation =
            from left in Identifier.Or(Terminal)
            from ws in Chars.Space()
            from right in Parse.Choose(Concatenation.Try(), Identifier, Terminal)
            select new Concatenation(left, right);

      private static IParser<NonTerminal> Alternation =
            from left in Tokens.Lexeme(Parse.Choose(Concatenation.Try(), Identifier, Terminal))
            from bar in Tokens.Symbol('|')
            from right in RightHandSide
            select new Alternation(left, right);

      private static IParser<NonTerminal> RightHandSide = 
            Parse.Choose<NonTerminal>( Alternation.Try()
                                     , Concatenation.Try()
                                     , Identifier
                                     , Terminal);

      private static IParser<Rule> Rule = 
            from ws in Tokens.WhiteSpace()
            from identifier in Tokens.Lexeme(Identifier)
            from assignment in Tokens.Lexeme(Assignment)
            from value in Tokens.Lexeme(RightHandSide)
            select new Rule((Identifier)identifier, value);
   }
}
