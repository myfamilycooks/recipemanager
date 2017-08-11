using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BistroFiftyTwo.Server.Parser
{
    public class Lexer
    {
        public static Token Tokenize(string input)
        {
            var parts = Regex.Split(input, @"\s+");
            var tokens = new List<string>();
            var token = new Token();

            if(parts != null && parts.Any())
            {
                tokens = parts.ToList();
            }

            tokens.ForEach(t =>
            {
                if (!String.IsNullOrEmpty(t))
                {
                    var newToken = new Token()
                    {
                        Previous = token
                    };

                    token.Value = t;
                    token.Next = newToken;
                    token = token.Next;
                }
            });

            while (token.Previous != null)
                token = token.Previous;

            return token;
        }
    }
}