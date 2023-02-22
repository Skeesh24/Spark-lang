namespace Spark_lang.AST
{
    public class AssignNode : ExpressionNode
    {
        public Token Operator;

        public ExpressionNode LeftNode;

        public ExpressionNode RightNode;

        public AssignNode(Token @operator, ExpressionNode leftNode, ExpressionNode rightNode)
        {
            Operator = @operator;
            LeftNode = leftNode;
            RightNode = rightNode;
        }
    }
}
