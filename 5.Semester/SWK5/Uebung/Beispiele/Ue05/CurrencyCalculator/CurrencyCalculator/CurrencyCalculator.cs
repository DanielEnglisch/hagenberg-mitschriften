using CurrencyCalculator.BL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyCalculator
{

    public enum Conversion
    {
        LeftToRight,
        RightToLeft
    }

    public class CurrencyCalculatorWindow : Window
    {

        private TextBox txt_LeftValue, txt_RightValue;
        private ComboBox cb_LeftCurrency, cb_RightCurrency;
        private ICurrencyCalculator calculator;

        public CurrencyCalculatorWindow()
        {
            txt_LeftValue = new TextBox() { Width = 80 };
            txt_RightValue = new TextBox() { Width = 80 };
            cb_LeftCurrency = new ComboBox() { Margin = new Thickness(5, 0, 5, 0) };
            cb_RightCurrency = new ComboBox() { Margin = new Thickness(5, 0, 0, 0) };

            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            panel.Children.Add(txt_LeftValue);
            panel.Children.Add(cb_LeftCurrency);
            panel.Children.Add(txt_RightValue);
            panel.Children.Add(cb_RightCurrency);

            this.Content = panel;
            this.Title = "WPF Currency Converter";

            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ResizeMode = ResizeMode.NoResize;

            this.calculator = CurrencyCalculatorFactory.GetCalculator();

            foreach (CurrencyData currency in calculator.GetCurrencyData())
            {
                cb_LeftCurrency.Items.Add(currency);
                cb_RightCurrency.Items.Add(currency);
            }

            cb_LeftCurrency.SelectedIndex = cb_RightCurrency.SelectedIndex = 0;

            cb_LeftCurrency.SelectionChanged += this.OnSelectionChanged;
            cb_RightCurrency.SelectionChanged += this.OnSelectionChanged;

            txt_LeftValue.TextChanged += OnTextChanged;
            txt_RightValue.TextChanged += OnTextChanged;

        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender == txt_LeftValue && txt_LeftValue.IsFocused)
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

        [STAThread]
        public static void Main(String[] args)
        {
            Application app = new Application();

            Window window = new CurrencyCalculatorWindow();
            window.Show();

            //Window window2 = new CurrencyCalculatorWindow();
            //window2.Title += "2";
            //window2.Show();

            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run();
        }
    }
}
