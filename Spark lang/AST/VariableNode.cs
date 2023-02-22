namespace Spark_lang.AST
{
    public class VariableNode : ExpressionNode
    {
        public Token Variable;

        public VariableNode(Token variable) : base()
        {
            Variable = variable;
        }
    }
}
