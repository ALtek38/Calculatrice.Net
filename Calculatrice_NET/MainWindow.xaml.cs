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

            Display = "";
            CurrentExpression = "";
            History = new ObservableCollection<HistoryItem>();
        }

        public string Display {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string CurrentExpression
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<HistoryItem> History
        {
            get { return GetValue<ObservableCollection<HistoryItem>>(); }
            set { SetValue(value); }
        }

        // Méthodes

        public string Calcule(string expression)
        {
            //return new System.Data.DataTable().Compute(expression, null).ToString();
            return Evaluator.Calculate(ShuntingYard.Parse(expression)).ToString();
        }

        public void ShowError(string message="Erreur")
        {
            MessageBox.Show(message, "Erreur");
        }

        private void Operator_Button_Click(Object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            Display += content;
            CurrentExpression += " " + content + " ";
        }
        private void Numeric_Button_Click(Object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();

            Display += content;
            CurrentExpression += content
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Display != null && Display.Length > 0)
            {
                // On supprime le dernier caractère du calcul
                Display = Display.Remove(Display.Length - 1);
                CurrentExpression = CurrentExpression.Remove(CurrentExpression.Length - 1);
            }
        }

        private void Compute_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Display != null && Display.Length > 0)
            {
                // Réalisation du calcul
                try
                {
                    // Réalisation du calcul
                    var result = Calcule(CurrentExpression);
                    SaveToHistory(Display, result, CurrentExpression);
                    Display = result;
                    CurrentExpression = "";
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void SaveToHistory(string compute, string result, string expression)
        {
            History.Add(new HistoryItem(compute, result, expression));
        }

        private void ClearHistory_Button_Click(object sender, RoutedEventArgs e)
        {
            History.Clear();
        }

        private void ReloadHistoryItem(object sender, SelectionChangedEventArgs args)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                HistoryItem itemToReload = ((sender as ListBox).SelectedItem as HistoryItem);
                Display = itemToReload.Compute;
                CurrentExpression = itemToReload.Expression;
            }
        }

        private void Inverse_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Display != null && Display.Length > 0 && Calcule(Display) != "0")
                {
                    Display = "1/(" + Display + ")";
                }
                else if (Calcule(Display) == "0")
                {
                    ShowError("Erreur: Division par zéro");
                }
            } catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ToggleHistory_Button_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryListBox.Visibility == Visibility.Hidden) {
                HistoryListBox.Visibility = Visibility.Visible;
            } else {
                HistoryListBox.Visibility = Visibility.Hidden;
            }
        }
    }
}
