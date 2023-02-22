namespace Spark_lang.AST
{
    public class BinOperationNode : ExpressionNode
    {
        public Token Operator;

        public ExpressionNode LeftNode;

        public ExpressionNode RightNode;

        public BinOperationNode(Token @operator, ExpressionNode leftNode, ExpressionNode rightNode) : base()
        {
            Operator = @operator;
            LeftNode = leftNode;
            RightNode = rightNode;
        }
    }
}
