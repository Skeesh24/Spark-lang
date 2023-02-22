using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spark_lang
{
    public class Lexer
    {
        private readonly List<TokenType> _tokenTypeList = new List<TokenType>()
                .Append(TokenType.NUMBER)
                .Append(TokenType.VARIABLE)
                .Append(TokenType.LOG)
                //.Append(TokenType.SPACE)
                .Append(TokenType.SEMICOLON)
                .Append(TokenType.PLUS)
                .Append(TokenType.MINUS)
                .Append(TokenType.ASSIGN)
                .Append(TokenType.LPAR)
                .Append(TokenType.RPAR)
                .ToList();

        public List<TokenType> GetTokenTypeList() => _tokenTypeList;

        public string? Code;

        public int Pos = 0;

        public Token[] TokenList = Array.Empty<Token>();

        public Lexer(string? code)
        {
            Code = code?.Replace('\r', ' ').Replace('\n', ' ').Replace(" ", "");
        }

        public Token[] LexAnalysis()
        {
            while(this.NextToken())
            {
                // что то можно прикрутить во время выполнения лексического анализатора
            }
            
            return TokenList;
        }

        public bool NextToken()
        {
            if(this.Pos >= this?.Code?.Length)
            {
                return false;
            }

            for (int i = 0; i < _tokenTypeList.Count; i++)
            {
                var tokenType = _tokenTypeList[i];
                var regex = new Regex("^" + tokenType.Regex);
                var result = regex.Match(this?.Code?.Substring(this.Pos));
                if (result != null && result.Length > 0)
                {
                    var token = new Token(tokenType, result.Value, this.Pos);
                    this.Pos += result.Value.Length;
                    this.TokenList = this.TokenList.Append(token).ToArray();
                    return true;
                }
            }
            throw new Exception($"На позиции {this.Pos} обнаружена ошибка");
        } 
    }
}
