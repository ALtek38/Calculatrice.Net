using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice_NET
{
    public static class ShuntingYard
    {
        private static readonly Dictionary<string, Operator> Operators
            = new Operator[] {
                new Operator("^", 4, true),
                new Operator("*", 3, false),
                new Operator("/", 3, false),
                new Operator("+", 2, false),
                new Operator("-", 2, false)
            }.ToDictionary(op => op.Symbol);

        public static string Parse(this string calcul)
        {
            string[] tokens = calcul.Split(' ');
            var stack = new Stack<string>();
            var output = new List<string>();

            foreach (string token in tokens)
            {
                if (int.TryParse(token, out _))
                {
                    output.Add(token);
                }
                else if (Operators.TryGetValue(token, out var op1))
                {
                    while (stack.Count > 0 && Operators.TryGetValue(stack.Peek(), out var op2))
                    {
                        int c = op1.Precedence.CompareTo(op2.Precedence);
                        if (c < 0 || !op1.RightAssociative && c <= 0)
                        {
                            output.Add(stack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    stack.Push(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    string top = "";
                    while (stack.Count > 0 && (top = stack.Pop()) != "(")
                    {
                        output.Add(top);
                    }
                    if (top != "(") throw new ArgumentException("Il manque une parenthèse gauche!");
                }
            }
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                if (! Operators.ContainsKey(top)) throw new ArgumentException("Il manque une parenthèse droite!");
                output.Add(top);
            }
            Console.WriteLine(string.Join(" ", output));
            return string.Join(" ", output);
        }
    }
}