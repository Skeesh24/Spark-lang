namespace Spark_lang.AST
{
    public class StatementsNode : ExpressionNode
    {
        public ExpressionNode[] CodeStrings = Array.Empty<ExpressionNode>();


        public void AddNode(ExpressionNode node)
        {
            CodeStrings = CodeStrings.Append(node).ToArray();
        }
    }
}
