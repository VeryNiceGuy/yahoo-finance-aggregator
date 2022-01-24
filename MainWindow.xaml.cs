using System;
using System.Windows;
using Microsoft.Win32;

namespace Aggregator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            string symbol = SymbolTextBox.Text;
            string period = PeriodComboBox.Text;

            var fromLocal = FromDatePicker.SelectedDate;
            var toLocal = ToDatePicker.SelectedDate;

            if (fromLocal == null || toLocal == null)
            {
                MessageBox.Show("Please select both dates.");
                return;
            }

            var offset = TimeZoneInfo.Local.GetUtcOffset(fromLocal.Value).Hours;
            var from = fromLocal.Value.AddHours(offset);
            var to = toLocal.Value.AddHours(offset);

            if (symbol.Length == 0)
            {
                MessageBox.Show("Symbol cannot be empty.");
                return;
            }

            if (from.CompareTo(to) > 0)
            {
                MessageBox.Show("From cannot be greater than To");
                return;
            }

            var now = DateTime.Now.AddHours(offset);

            if (from.CompareTo(now) > 0 || to.CompareTo(now) > 0)
            {
                MessageBox.Show("Neither date can be greater than today");
                return;
            }

            var history = await Loader.Load(symbol, from, to);
            if(history == null)
            {
                MessageBox.Show("Invalid request.");
                return;
            }

            var records = Converter.ConvertTo(period, history);

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Quotes (*.pdf)|*.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                Publisher.Publish(dialog.FileName, records);
            }
        }
    }
}
