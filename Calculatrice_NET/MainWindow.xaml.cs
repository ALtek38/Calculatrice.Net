using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Calculatrice_NET
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        public T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (_propertyValues.ContainsKey(propertyName))
                return (T)_propertyValues[propertyName];
            return default(T);
        }
        public bool SetValue<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            var currentValue = GetValue<T>(propertyName);
            if (currentValue == null && newValue != null
             || currentValue != null && !currentValue.Equals(newValue))
            {
                _propertyValues[propertyName] = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
        #endregion

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public string Display {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (sender.ToString())
            {
                // Numeric values
                case "System.Windows.Controls.Button: 1":
                    Display += "1";
                    break;
                case "System.Windows.Controls.Button: 2":
                    Display += "2";
                    break;
                case "System.Windows.Controls.Button: 3":
                    Display += "3";
                    break;
                case "System.Windows.Controls.Button: 4":
                    Display += "4";
                    break;
                case "System.Windows.Controls.Button: 5":
                    Display += "5";
                    break;
                case "System.Windows.Controls.Button: 6":
                    Display += "6";
                    break;
                case "System.Windows.Controls.Button: 7":
                    Display += "7";
                    break;
                case "System.Windows.Controls.Button: 8":
                    Display += "8";
                    break;
                case "System.Windows.Controls.Button: 9":
                    Display += "9";
                    break;
                // Operators
                case "System.Windows.Controls.Button: +":
                    Display += "+";
                    break;
                case "System.Windows.Controls.Button: -":
                    Display += "-";
                    break;
                case "System.Windows.Controls.Button: /":
                    Display += "/";
                    break;
                case "System.Windows.Controls.Button: *":
                    Display += "*";
                    break;
                // Others
                case "System.Windows.Controls.Button: (":
                    Display += "(";
                    break;
                case "System.Windows.Controls.Button: )":
                    Display += ")";
                    break;
                case "System.Windows.Controls.Button: Supprimer":
                    Display = "";
                    break;
                case "System.Windows.Controls.Button: =":
                    Display = new System.Data.DataTable().Compute(Display, null).ToString();
                    break;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display != null && Display.Length > 0 )
            {
                Display = Display.Remove(Display.Length - 1);
            }
        }
    }
}
