﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Calculatrice_NET
{
    class Evaluator
    {
        /*static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            string rpn = "3 4 2 * 1 5 - 2 3 ^ ^ / +";
            Console.WriteLine("{0}\n", rpn);

            decimal result = Calculate(rpn);
            Console.WriteLine("\nResult is {0}", result);
        }*/

        public static decimal Calculate(string parsedCalculation)
        {
            string[] rpnTokens = parsedCalculation.Split(' ');
            Stack<decimal> stack = new Stack<decimal>();
            decimal number = decimal.Zero;

            foreach (string token in rpnTokens)
            {
                if (decimal.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    switch (token)
                    {
                        case "^":
                        case "pow":
                            {
                                number = stack.Pop();
                                stack.Push((decimal)Math.Pow((double)stack.Pop(), (double)number));
                                break;
                            }
                        case "ln":
                            {
                                stack.Push((decimal)Math.Log((double)stack.Pop(), Math.E));
                                break;
                            }
                        case "sqrt":
                            {
                                stack.Push((decimal)Math.Sqrt((double)stack.Pop()));
                                break;
                            }
                        case "*":
                            {
                                stack.Push(stack.Pop() * stack.Pop());
                                break;
                            }
                        case "/":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() / number);
                                break;
                            }
                        case "+":
                            {
                                stack.Push(stack.Pop() + stack.Pop());
                                break;
                            }
                        case "-":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() - number);
                                break;
                            }
                        default:
                            Console.WriteLine("Impossible de réaliser ce calcul!");
                            break;
                    }
                }
            }

            return stack.Pop();
        }
    }
}
