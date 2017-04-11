using System;
using System.Timers;

namespace SmokeyLib
{
    internal class RepeatedFunction
    {
        private Action _function;
        private Timer _timer;

        public RepeatedFunction(Action function, double interval)
        {
            _function = function;
            Reset(interval);
        }

        public void Reset(double interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += _timerElapsed;
            _timer.AutoReset = true;
        }

        public void Start()
        {
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
        }

        private void _timerElapsed(object sender, ElapsedEventArgs e)
        {
            _function?.Invoke();
        }
    }
}
