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
            History = new ObservableCollection<HistoryItem>();
        }

        public string Display {
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
            return new System.Data.DataTable().Compute(expression, null).ToString();
        }

        public void ShowError(string message="Erreur")
        {
            MessageBox.Show(message, "Erreur");
        }

        // Gestion des clics des boutons

        // Affichage des valeurs dans l'écran de calcul
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (sender.ToString())
            {
                // Numeric values
                case "System.Windows.Controls.Button: 0":
                    Display += "0";
                    break;
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
                case "System.Windows.Controls.Button: ,":
                    Display += ".";
                    break;
                case "System.Windows.Controls.Button: Supprimer":
                    Display = "";
                    break;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display != null && Display.Length > 0 )
            {
                // On supprime le dernier caractère du calcul
                Display = Display.Remove(Display.Length - 1);
            }
        }

        private void ComputeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Display != null && Display.Length > 0)
            {
                // Réalisation du calcul
                try
                {
                    // Réalisation du calcul
                    var compute = Display;
                    var result = Calcule(Display);
                    Display = result;
                    SaveToHistory(compute, result);
                    Display = "";
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void SaveToHistory(string compute, string result)
        {
            History.Add(new HistoryItem(compute, result));
        }

        private void ClearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            History.Clear();
        }

        private void ReloadHistoryItem(object sender, SelectionChangedEventArgs args)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                HistoryItem itemToReload = ((sender as ListBox).SelectedItem as HistoryItem);
                Display = itemToReload.Compute;
            }
        }

        private void InverseButton_Click(object sender, RoutedEventArgs e)
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

        private void ToggleHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryListBox.Visibility == Visibility.Hidden) {
                HistoryListBox.Visibility = Visibility.Visible;
            } else {
                HistoryListBox.Visibility = Visibility.Hidden;
            }
        }
    }
}
