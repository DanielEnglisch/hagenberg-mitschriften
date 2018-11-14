using System;
using System.Threading;

namespace Timer
{
    class Timer
    {
        private Thread tickerThread;
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(1);

        // Dieses Dalagate können mehrere Methoden mit += zugewiesen werden
        // Keyword event verwindert überschreiben bereits registrierter methoden
        public event ExpiredEventHandler Expired;

        public Timer()
        {
            // Delegate welches eine void Methode als Parameter nimmt
            // ThreadStart start = this.Run;
            // tickerThread = new Thread(start);

            tickerThread = new Thread(this.Run);
        }

        public void Start()
        {
            this.tickerThread.Start();
        }

        private void Run()
        {
            while (true)
            {
                Thread.Sleep(this.Interval);

                // Aufruf der in der Delegate registrierten Methods

                /*
                if (this.Expired != null)
                {
                    // Früher
                    // Expired.Invoke(DateTime.Now);
                    Expired(DateTime.Now);
                }
                */

                // Wenn Expired nicht null ist rufe Invoke an
                this.Expired?.Invoke(DateTime.Now);
            }

        }
    }
}
