using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice_NET
{
    class Operator
    {
        private string symbol;
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        private int precedence;
        public int Precedence
        {
            get { return precedence; }
            set { precedence = value; }
        }

        private bool rightAssociative;
        public bool RightAssociative
        {
            get { return rightAssociative; }
            set { rightAssociative = value; }
        }

        public Operator(string symbol, int precedence, bool rightAssociative)
        {
            Symbol = symbol;
            Precedence = precedence;
            RightAssociative = rightAssociative;
        }
    }
}
