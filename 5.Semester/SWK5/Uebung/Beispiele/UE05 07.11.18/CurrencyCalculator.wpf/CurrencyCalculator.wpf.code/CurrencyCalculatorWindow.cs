using CurrencyCalculator.BL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyCalculator.wpf.code
{
    enum Conversion
    {
        LeftToRight, RightToLeft
    }

    class CurrencyCalculatorWindow : Window
    {
        private TextBox txtLeftValue;
        private TextBox txtRightValue;
        private ComboBox cmbLeftCurrency;
        private ComboBox cmbRightCurrency;

        private ICurrencyCalculator calculator;

        public CurrencyCalculatorWindow()
        {
            txtLeftValue = new TextBox() { Width = 80 };
            txtRightValue = new TextBox() { Width = 80};
            cmbLeftCurrency = new ComboBox() { Margin = new Thickness(5, 0, 5, 0) };
            cmbRightCurrency = new ComboBox() { Margin = new Thickness(5, 0, 0, 0) };

            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10)
            };

            panel.Children.Add(txtLeftValue);
            panel.Children.Add(cmbLeftCurrency);
            panel.Children.Add(txtRightValue);
            panel.Children.Add(cmbRightCurrency);

            this.Content = panel;
            this.Title = "WPF Currency Converter (Code)";

            // wie sich der content des windows anpassen soll
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ResizeMode = ResizeMode.NoResize;

            this.calculator = CurrencyCalculatorFactory.GetCalculator();

            foreach (CurrencyData currency in calculator.GetCurrencyData())
            {
                cmbLeftCurrency.Items.Add(currency);
                cmbRightCurrency.Items.Add(currency);
            }

            cmbLeftCurrency.SelectedIndex = cmbRightCurrency.SelectedIndex = 0;

            cmbLeftCurrency.SelectionChanged += OnSelectionChanged;
            cmbRightCurrency.SelectionChanged += OnSelectionChanged;

            txtLeftValue.TextChanged += OnTextChanged;
            txtRightValue.TextChanged += OnTextChanged;
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

        [STAThread]
        public static void Main(string[] args)
        {
            // singleton
            Application app = new Application();

            // beim erstellen von window wird nachgesehen ob es eine current application gibt
            // falls ja registriert sich window bei der application
            Window window = new CurrencyCalculatorWindow();
            window.Show();

            //Window window2 = new CurrencyCalculatorWindow();
            //window2.Title += "2";
            //window2.Show();

            //ShutdownMode.OnExplicitShutdown ||ShutdownMode.OnMainWindowClose 
            app.ShutdownMode = ShutdownMode.OnLastWindowClose;
            app.Run();
        }
    }
}
