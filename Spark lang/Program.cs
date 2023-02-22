namespace Spark_lang
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var code = @"
                        code=8-5;
                        yupi = 5+7;
                        << yupi;
                        << 255+(745-700);
                        ";

            var lexer = new Lexer(code);

            lexer.LexAnalysis();

            var parser = new Parser(lexer.TokenList);

            var rootNode = parser.ParseCode();

            parser.Run(rootNode);


            //parser.Scope.Select(x => $"{x.Key}: {x.Value}").ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}