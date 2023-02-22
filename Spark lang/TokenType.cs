namespace Spark_lang
{
    public class TokenType
    {
        public string? Name;

        public string? Regex;

        public TokenType(string? name, string? regex)
        {
            this.Name = name;
            this.Regex = regex;
        }


        public static readonly TokenType NUMBER = new("NUMBER", "[0-9]*");

        public static readonly TokenType VARIABLE = new("VARIABLE", "[a-z]*");

        public static readonly TokenType SEMICOLON = new("SEMICOLON", ";");

        //public static readonly TokenType SPACE = new("SPACE", "[ \\n\\t\\r]");

        public static readonly TokenType ASSIGN = new("ASSIGN", "=");

        public static readonly TokenType LOG = new("LOG", "<<");

        public static readonly TokenType PLUS = new("PLUS", "\\+");

        public static readonly TokenType MINUS = new("MINUS", "-");

        public static readonly TokenType LPAR = new("LPAR", "\\(");

        public static readonly TokenType RPAR = new("RPAR", "\\)");
    }
}
