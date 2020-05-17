using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice_NET
{
    public class HistoryItem : BaseNotifyPropertyChanged
    {
        public string Compute
        {
            get { return GetValue<String>(); }
            set { SetValue(value);  }
        }

        public string Expression
        {
            get { return GetValue<String>(); }
            set { SetValue(value); }
        }

        public string Result
        {
            get { return GetValue<String>(); }
            set { SetValue(value); }
        }

        public HistoryItem(string compute, string result, string expression)
        {
            Compute = compute;
            Result = result;
            Expression = expression;
        }

        public override string ToString()
        {
            return Compute + " = " + Result;
        }
    }
}
