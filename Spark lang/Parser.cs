using Spark_lang.AST;

namespace Spark_lang
{
    public class Parser
    {
        public Token[]? Tokens;

        public int Pos = 0;

        public Dictionary<string, int> Scope = new Dictionary<string, int>();

        public Parser(Token[]? tokens)
        {
            Tokens = tokens;
        }

        public Token? Match(TokenType[] expected)
        {
            if(this.Pos < this?.Tokens?.Length)
            {
                var currentToken = this.Tokens[this.Pos];
                if(expected.Where(type => type.Name == currentToken?.Type?.Name).FirstOrDefault() != null)
                {
                    this.Pos++;
                    return currentToken;
                }
            }
            return null;
        }

        public Token Require(TokenType[] expected)
        {
            var token = this.Match(expected);
            if (token == null)
            {
                throw new Exception($"На позиции {this.Pos} ожидалось {expected?.FirstOrDefault()?.Name}");
            }
            return token;
        }

        public ExpressionNode ParseVariableOrNumber()
        {
            var number = this.Match(new[] { TokenType.NUMBER });
            if(number != null)
                return new NumberNode(number);
            var variable = this.Match(new[] { TokenType.VARIABLE });

            if(variable != null)
                return new VariableNode(variable);

            throw new Exception($"На позиции {this.Pos} ожидалось значение или переменная");
        }

        public ExpressionNode ParsePrint()
        {
            var operatorLog = this.Match(new[] { TokenType.LOG });

            if (operatorLog != null)
                return new UnarOperationNode(operatorLog, this.ParseFormula());
            throw new Exception($"На позиции {this.Pos} ожидалась функция");
        }

        public ExpressionNode ParseParentheses()
        {
            if(this.Match(new[] { TokenType.LPAR } ) != null)
            {
                var node = this.ParseFormula();
                this.Require(new[] { TokenType.RPAR });
                return node;
            }
            else
            {
                return this.ParseVariableOrNumber();
            }
        }

        public ExpressionNode ParseFormula()
        {
            var leftNode = this.ParseParentheses();
            var @operator = this.Match(new[] { TokenType.MINUS, TokenType.PLUS });
            while(@operator != null)
            {
                var rightNode = this.ParseParentheses();
                leftNode = new BinOperationNode(@operator, leftNode, rightNode);
                @operator = this.Match(new[] { TokenType.MINUS, TokenType.PLUS });
            }
            return leftNode;
        }

        public ExpressionNode ParseExpression()
        {
            if (this.Match(new[] { TokenType.VARIABLE }) == null)
                return this.ParsePrint();
            this.Pos --;
            var variableNode = this.ParseVariableOrNumber();
            var assignOPerator = this.Match(new[] { TokenType.ASSIGN });
            if(assignOPerator != null)
            {
                var rightFormula = this.ParseFormula();
                var binaryNode = new BinOperationNode(assignOPerator, variableNode, rightFormula);
                return binaryNode;
            }
            throw new Exception($"После переменной ожидается оператор присвоения на позиции ${{this.pos}}");
        }

        public ExpressionNode ParseCode()
        {
            var root = new StatementsNode();
            while(this.Pos < this?.Tokens?.Length)
            {
                var codeStringNode = this.ParseExpression();
                this.Require(new[] { TokenType.SEMICOLON });
                root.AddNode(codeStringNode);
            }
            return root;
        }


        public object Run(ExpressionNode node)
        {
            if(node is NumberNode)
            {
                var numNode = ((NumberNode)node);
                return int.Parse(numNode?.Number?.Text);
            }

            else if(node is UnarOperationNode)
            {
                var unarnode = (UnarOperationNode)node;
                if(unarnode?.Operator?.Type?.Name == TokenType.LOG.Name)
                { 
                    Console.WriteLine(this.Run(unarnode.Operand));
                }
            }

            else if(node is BinOperationNode)
            {
                var binOperationNode = (BinOperationNode)node;
                if (binOperationNode?.Operator?.Type?.Name == TokenType.PLUS.Name)
                    return (int)this.Run(binOperationNode?.LeftNode) + (int) this.Run(binOperationNode.RightNode);
                if(binOperationNode?.Operator?.Type?.Name == TokenType.MINUS.Name)
                        return (int)this.Run(binOperationNode?.LeftNode) - (int)this.Run(binOperationNode.RightNode);
                if(binOperationNode?.Operator?.Type?.Name == TokenType.ASSIGN.Name)
                {
                    var result = (int)this.Run(binOperationNode?.RightNode);
                    var variableNode = binOperationNode.LeftNode as VariableNode;
                    this.Scope.Add(variableNode.Variable.Text, result);
                    return result;
                }
            }

            else if(node is VariableNode)
            {
                var variableNode = (VariableNode)node;
                if (this?.Scope[variableNode?.Variable?.Text] != null)
                {
                    return this.Scope[variableNode.Variable.Text];
                }
                else
                {
                    throw new Exception($"Переменная с названием ${variableNode.Variable.Text} не обнаружена");
                }
            }

            else if(node is StatementsNode)
            {
                var statementsNode = (StatementsNode)node;
                statementsNode.CodeStrings.ToList().ForEach(codeString => this.Run(codeString));
                return null;
            }

            return new Exception("Critical error!");
        }

    }
}
