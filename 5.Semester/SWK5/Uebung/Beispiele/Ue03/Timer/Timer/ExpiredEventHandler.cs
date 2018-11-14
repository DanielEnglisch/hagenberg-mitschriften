using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    // Definition eines eigenen dalegates, welches aufgerufen wird bei Ablauf des Timers
    public delegate void ExpiredEventHandler(DateTime signaledTime);
}
