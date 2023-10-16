using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TaiSim.Fumen;

/*
 * explanation: based heavily on simai syntax
 * (160) {8} r,b,b,r,b,{16}r,r+,r,r+,R[4:1],E
 * # this comments the entire line
 */

public class Serialiser
{
    enum Token
    {
        Seperator,  // ,
        LeftParen,  // (
        RightParen, // )
        LeftBrce,   // {
        RightBrce,  // }
        LeftSq,     // [
        RightSq,    // ]
        Red,        // r
        Blue,       // b
        Roll,       // R
        Plus,       // +
        Colon,      // :
        EndMarker,  // E
        Comment,    // #
    }
    
    class Tokeniser
    {
        class ParseException : Exception
        {
            public ParseException(int pos) 
                : base($"[TOKENISER]: no such match for token starting at {pos}") 
            { }
        }
        
        private string fumen;
        private List<(Token, string)> definitions;
        
        public Tokeniser(string fumen)
        {
            this.fumen = fumen;
            definitions = new List<(Token, string)>(new[]
            {
                (Token.Seperator,  @","),
                (Token.LeftParen,  @"("),
                (Token.RightParen, @")"),
                (Token.LeftBrce,   @"["),
            });
        }

        public List<Token> Tokenise()
        {
            char[] chars = fumen.ToCharArray();
            StringBuilder sb = new StringBuilder();
            List<Token> tokens = new List<Token>();
            for (int i = 0; i < chars.Length; )
            {
                bool found = false;
                for (int j = i; j < chars.Length; j++)
                {
                    sb.Append(chars[i]);
                    foreach (var (token, pattern) in definitions)
                    {
                        var m = Regex.Match(sb.ToString(), pattern);
                        if (m.Success)
                        {
                            i += m.Length + 1;
                            sb.Clear();
                            found = true;
                            tokens.Add(token);
                            break;
                        }
                    }

                    if (found)
                        break;
                }

                if (!found)
                    throw new ParseException(i);
            }

            return tokens;
        }
    }

    /**
     * the grammar
     
     TODO:
     
     */
    
    class Parser
    {
        private Token[] tokens;
        
        public Parser(Token[] tokens)
        {
            this.tokens = tokens;
        }

        public LogicalTimeline Parse()
        {
            throw new NotImplementedException();
        }
    }

    private string fumen;

    public Serialiser(string input)
    {
        fumen = input;
    }

    public LogicalTimeline Serialise()
    {
        var tokeniser = new Tokeniser(fumen);
        var tokens = tokeniser.Tokenise();
        var parser = new Parser(tokens.ToArray());
        return parser.Parse();
    }
}