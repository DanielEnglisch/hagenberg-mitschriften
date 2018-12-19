using System;

namespace Timer_Delegate
{
    // Definition eines eigenen dalegates, welches aufgerufen wird bei Ablauf des Timers
    // 1.
    public delegate void ExpiredEventHandler(DateTime signaledTime);
}
