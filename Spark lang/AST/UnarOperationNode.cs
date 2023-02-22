namespace Spark_lang.AST
{
    public class UnarOperationNode : ExpressionNode
    {
        public Token Operator;
        public ExpressionNode Operand;

        public UnarOperationNode(Token @operator, ExpressionNode operand)
        {
            Operator = @operator;
            Operand = operand;
        }
    }
}
