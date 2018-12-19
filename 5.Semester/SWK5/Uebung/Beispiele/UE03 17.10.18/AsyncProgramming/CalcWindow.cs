using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncProgramming
{
    public partial class CalcWindow : Form
    {
        private const int NO_ITEMS = 1000000000;

        public CalcWindow()
        {
            InitializeComponent();
        }

        private void DisableButtons()
        {
            btnSynchronous.Enabled = btnThread.Enabled = btnTask.Enabled = btnAwaitAsync.Enabled = false;
        }

        private void EnableButtons()
        {
            btnSynchronous.Enabled = btnThread.Enabled = btnTask.Enabled = btnAwaitAsync.Enabled = true;
        }

        private long CalcSum()
        {
            long sum = 0;
            for (int i = 0; i < NO_ITEMS; i++)
                sum += i;
            return sum;
        }

        private void SynchronousButtonHandler(object sender, EventArgs e)
        {
            txtResult.Text = "";
            DisableButtons();

            txtResult.Text = CalcSum().ToString();

            EnableButtons();
        }

        private void ThreadButtonHandler(object sender, EventArgs e)
        {
            txtResult.Text = "";
            DisableButtons();

            Thread worker = new Thread(
                () =>
                {
                    long sum = CalcSum();

                    // warum new action und nicht lambda? Weil Invoke die Delegate Basisklasse
                    // verwendet und eine Lambda Epression ist nicht vom Typ Delegate ist ( != delegate)
                    // https://docs.microsoft.com/en-us/dotnet/api/system.delegate?redirectedfrom=MSDN&view=netframework-4.7.2
                    txtResult.Invoke(
                        new Action(() =>
                        {
                            txtResult.Text = sum.ToString();
                            EnableButtons();
                        }
                        ));
                });
            worker.Start();
        }

        private Task<long> CalcSumAsync()
        {
            return Task.Run(() => CalcSum());
        }

        private void TaskButtonHandler(object sender, EventArgs e)
        {
            txtResult.Text = "";
            DisableButtons();

            Task<long> calcTask = CalcSumAsync();

            // Aktuellen Context (UI Thread) speichern
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            calcTask.ContinueWith(task =>
            {
                // Diese continuation action wird im gleichen thread wie der
                // task ausgeführt daher gibts eine exception
                // ! Da jedoch der scheduler bei ContinueWith mitgegeben wurde
                // wird ContinueWith mit dem Scheduler durchgeführt -> UI Thread
                 txtResult.Text = calcTask.Result.ToString();
                 EnableButtons();
                
            }, scheduler);
        }

        // Bei await muss async verwendet werden
        private async void AsyncAwaitButtonHandler(object sender, EventArgs e)
        {
            txtResult.Text = "";
            DisableButtons();

            // einfache version:
            // await liefert auch das resultat des Tasks
            txtResult.Text = (await CalcSumAsync()).ToString();
            EnableButtons();

            /*
              Task<long> calcTask = CalcSumAsync();

               await -> wartet bis der Task fertig ist aber blockiert nicht.
               Es wird im prinzip ein Callback generiert das erst ausgeführt wird
               wenn der task fertig ist. Danach wird der restliche Code ausgeführt.

               var sum = await calcTask;

               durch das await sind wir hier immer noch im UI Thread!

               txtResult.Text = sum.ToString();
               EnableButtons();
            */
        }

        private void CalcWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
