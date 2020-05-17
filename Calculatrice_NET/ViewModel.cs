using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculatrice_NET
{
    class ViewModel : INotifyPropertyChanged
    {
        private ICommand numericInput;
        public ICommand NumericInput
        {
            get
            {
                if (numericInput == null)
                {
                    numericInput = new RelayCommand(Numeric_Button_Click);
                }
                return numericInput;
            }
        }

        private ICommand operatorInput;
        public ICommand OperatorInput
        {
            get
            {
                if (operatorInput == null)
                {
                    operatorInput = new RelayCommand(Operator_Button_Click);
                }
                return operatorInput;
            }
        }
    }
}
