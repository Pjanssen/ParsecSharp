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
      public static Parser<IEnumerable<Rule>> Create()
      {
         return Rule.Many().FollowedBy(Parse.Eof<IEnumerable<Rule>>());
      }

      private static Parser<string> SymbolName =
            Chars.Satisfy(c => Char.IsLetterOrDigit(c) || c == '-')
                 .Many1();

      private static Parser<NonTerminal> Identifier =
            from open in Chars.Char('<')
            from symbol in SymbolName
            from close in Chars.Char('>')
            select (NonTerminal)new Identifier(symbol);

      private static Parser<string> Assignment = Chars.String("::=");

      private static Parser<NonTerminal> Terminal =
            from open in Chars.OneOf("\"'")
            from value in Chars.Not(open).Many()
            from close in Chars.Char(open)
            select (NonTerminal)new Terminal(value);

      private static Parser<NonTerminal> Concatenation =
            from left in Identifier | Terminal
            from ws in Chars.Space()
            from right in Parse.Try(Concatenation) | Identifier | Terminal
            select (NonTerminal)new Concatenation(left, right);

      private static Parser<NonTerminal> Alternation =
            from left in Tokens.Lexeme(Parse.Try(Concatenation) | Identifier | Terminal)
            from bar in Tokens.Symbol('|')
            from right in RightHandSide
            select (NonTerminal)new Alternation(left, right);

      private static Parser<NonTerminal> RightHandSide = Parse.Try(Alternation) 
                                                       | Parse.Try(Concatenation) 
                                                       | Identifier 
                                                       | Terminal;

      private static Parser<Rule> Rule = 
            from ws in Chars.WhiteSpace().Many()
            from identifier in Tokens.Lexeme(Identifier)
            from assignment in Tokens.Lexeme(Assignment)
            from value in Tokens.Lexeme(RightHandSide)
            select new Rule((Identifier)identifier, value);
   }
}
