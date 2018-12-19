using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Timer_Delegate
{
    class Timer
    {
        private Thread tickerThread;

        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(1);

        // mit dem event Schlüsselwort macht der Compiler im Hintergrund das Observer PAttern
        // also eine Collection von ExpiredEventHandler
        // bei Expired.Invoke wird dann über alle EventHandler iteriert und Invoke aufgerufen
        // 2.
        public event ExpiredEventHandler Expired;

        public Timer()
        {
            // threadstart ist ein delegate; rückgabewert void, ohne parameter
            // ThreadStart start = new ThreadStart(Run);
            tickerThread = new Thread(this.Run);
        }

        public void Start()
        {
            tickerThread.Start();
        }

        private void Run()
        {
            while (true)
            {
                Thread.Sleep(this.Interval);

               /* alte schreibweise
                if (Expired != null)
                    Expired(DateTime.Now);
                */

                // neue schreibweise, Methode nach ? wird nur aufgerufen
                // falls Expired nicht null
                // 3.
                Expired?.Invoke(DateTime.Now);
            }
        }
    }
}
