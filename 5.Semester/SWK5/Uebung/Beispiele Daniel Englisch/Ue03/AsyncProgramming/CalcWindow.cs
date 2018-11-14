using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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

            Thread worker = new Thread(() =>
            {
                long sum = CalcSum();
                txtResult.Invoke(new Action(() =>
                {
                    txtResult.Text = sum.ToString();
                    EnableButtons();

                }));
            });

            worker.Start();
        }

        private void TaskButtonHandler(object sender, EventArgs e)
        {
            txtResult.Text = "";
            DisableButtons();

            Task<long> calcTask = CalcSumAsync();

            // Die Action wird im UI Thread ausgeführt
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            // Task Callback
            calcTask.ContinueWith(task =>
            {
                txtResult.Text = task.Result.ToString();
                EnableButtons();
            }, scheduler);

            
        }

        private Task<long> CalcSumAsync()
        {
            return Task.Run(() => CalcSum());
        }

        private async void AsyncAwaitButtonHandler(object sender, EventArgs e)
        {
            txtResult.Text = "";
            DisableButtons();


            /*
                Task<long> calcTask = CalcSumAsync();
                var sum = await calcTask;
                txtResult.Text = sum.ToString();
            */

            txtResult.Text = (await CalcSumAsync()).ToString();


            EnableButtons();

        }
    }
}
