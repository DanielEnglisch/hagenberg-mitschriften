using CurrencyCalculator.BL;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CurrencyCalculator.Wpf.xaml
{
    public enum Conversion
    {
        LeftToRight,
        RightToLeft
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class CurrencyCalculatorWindow : Window
    {
        private ICurrencyCalculator calculator;

        public CurrencyCalculatorWindow()
        {
            InitializeComponent();

            calculator = CurrencyCalculatorFactory.GetCalculator();

            IEnumerable<CurrencyData> currencies = calculator.GetCurrencyData();
            cb_LeftCurrency.ItemsSource = cb_RightCurrency.ItemsSource = currencies;
            cb_LeftCurrency.SelectedItem = currencies.First(c => c.Symbol == "USD");
            cb_RightCurrency.SelectedItem = currencies.First(c => c.Symbol == "EUR");

        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == txt_LeftValue && txt_LeftValue.IsFocused)
                Convert(Conversion.LeftToRight);
            else if (sender == txt_RightValue && txt_RightValue.IsFocused)
                Convert(Conversion.RightToLeft);

        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == cb_LeftCurrency)
                Convert(Conversion.LeftToRight);
            else
                Convert(Conversion.RightToLeft);

            this.UpdateStatistics();

        }

        private void Convert(Conversion conversion)
        {
            if (cb_LeftCurrency.SelectedItem == null || cb_RightCurrency.SelectedItem == null)
                return;

            string leftCurrency = ((CurrencyData)cb_LeftCurrency.SelectedItem).Symbol;
            string rightCurrency = ((CurrencyData)cb_RightCurrency.SelectedItem).Symbol;

            double input;
            if (conversion == Conversion.LeftToRight)
            {
                if (double.TryParse(txt_LeftValue.Text, out input))
                    txt_RightValue.Text = calculator.Convert(input, leftCurrency, rightCurrency).ToString("F2");
            }
            else
            {
                if (double.TryParse(txt_RightValue.Text, out input))
                    txt_LeftValue.Text = calculator.Convert(input, rightCurrency, leftCurrency).ToString("F2");
            }
        }

        private void UpdateStatistics()
        {
            if (cb_LeftCurrency?.SelectedItem == null ||
                        cb_RightCurrency?.SelectedItem == null)
                return;

            string leftCurrency = ((CurrencyData)cb_LeftCurrency.SelectedItem).Symbol;
            string rightCurency = ((CurrencyData)cb_RightCurrency.SelectedItem).Symbol;
            DateTime to = DateTime.Now;
            DateTime from = to.AddMonths(-12);

            IEnumerable<RangeCurrencyData> avgRates =
              calculator.MonthlyRatesOfExchange(leftCurrency, rightCurency, from, to);
            double maxRate = avgRates.Max(rd => rd.AverageRate);

            chart.ItemsSource =
              avgRates.Select(rd => new {
                  Date = rd.From,
                  AverageRate = rd.AverageRate,
                  PercentRate = rd.AverageRate / maxRate * 100
              });
        }
    }
}
