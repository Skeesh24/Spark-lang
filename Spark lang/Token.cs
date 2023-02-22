namespace Spark_lang
{
    public  class Token
    {
        public TokenType? Type;

        public string? Text;

        public int Pos;

        public Token(TokenType? type, string? text, int pos)
        {
            this.Type = type;
            this.Text = text;
            this.Pos = pos;
        }
    }
}
