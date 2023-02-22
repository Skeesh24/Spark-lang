namespace Spark_lang.AST
{
    public class NumberNode : ExpressionNode
    {
        public Token Number;

        public NumberNode(Token number)
        {
            Number = number;
        }
    }
}
