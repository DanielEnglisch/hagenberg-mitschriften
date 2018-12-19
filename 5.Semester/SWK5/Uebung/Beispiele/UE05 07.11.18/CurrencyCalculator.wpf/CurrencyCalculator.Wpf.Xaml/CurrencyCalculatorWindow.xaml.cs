using CurrencyCalculator.BL;
using Swk5.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyCalculator.Wpf.Xaml
{
    internal enum Conversion
    {
        LeftToRight, RightToLeft
    }

    public partial class CurrencyCalculatorWindow : Window
    {
        private ICurrencyCalculator calculator;

        public string Country { get; set; }

        public CurrencyCalculatorWindow()
        {
            InitializeComponent();

            calculator = CurrencyCalculatorFactory.GetCalculator();

            IEnumerable<CurrencyData> currencies = calculator.GetCurrencyData();
            cmbLeftCurrency.ItemsSource = cmbRightCurrency.ItemsSource = currencies;

            cmbLeftCurrency.SelectedItem = currencies.First(c => c.Symbol == "EUR");
            cmbRightCurrency.SelectedItem = currencies.First(c => c.Symbol == "JPY");
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == txtLeftValue && txtLeftValue.IsFocused)
                Convert(Conversion.LeftToRight);
            else if (sender == txtRightValue && txtRightValue.IsFocused)
                Convert(Conversion.RightToLeft);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == cmbLeftCurrency)
                Convert(Conversion.LeftToRight);
            else
                Convert(Conversion.RightToLeft);

            UpdateStatistics();
        }

        private void Convert(Conversion conversion)
        {
            if (cmbLeftCurrency.SelectedItem == null || cmbRightCurrency.SelectedItem == null)
                return;

            string leftCurrency = ((CurrencyData)cmbLeftCurrency.SelectedItem).Symbol;
            string rightCurrency = ((CurrencyData)cmbRightCurrency.SelectedItem).Symbol;

            double input;
            if (conversion == Conversion.LeftToRight)
            {
                if (double.TryParse(txtLeftValue.Text, out input))
                    txtRightValue.Text = calculator.Convert(input, leftCurrency, rightCurrency).ToString("F2");
            }
            else
            {
                if (double.TryParse(txtRightValue.Text, out input))
                    txtLeftValue.Text = calculator.Convert(input, rightCurrency, leftCurrency).ToString("F2");
            }
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            using (StreamWriter writer = new StreamWriter("LogicalTree.txt"))
            {
                WpfUtil.DumpLogicalTree(this, writer);
            }

            using (StreamWriter writer = new StreamWriter("VisualTree.txt"))
            {
                WpfUtil.DumpVisualTree(this, writer);
            }
        }

        private void UpdateStatistics()
        {
            if (cmbLeftCurrency?.SelectedItem == null ||
                        cmbRightCurrency?.SelectedItem == null)
                return;

            string leftCurrency = ((CurrencyData)cmbLeftCurrency.SelectedItem).Symbol;
            string rightCurency = ((CurrencyData)cmbRightCurrency.SelectedItem).Symbol;
            DateTime to = DateTime.Now;
            DateTime from = to.AddMonths(-12);

            IEnumerable<RangeCurrencyData> avgRates =
              calculator.MonthlyRatesOfExchange(leftCurrency, rightCurency, from, to);
            double maxRate = avgRates.Max(rd => rd.AverageRate);

            chart.ItemsSource =
              avgRates.Select(rd => new
              {
                  Date = rd.From,
                  AverageRate = rd.AverageRate,
                  PercentRate = rd.AverageRate / maxRate * 100
              });
        }
    }
}