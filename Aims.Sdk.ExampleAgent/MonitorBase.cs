using System;
using System.Diagnostics;
using System.Threading;

namespace Aims.Sdk.ExampleAgent
{
    public abstract class MonitorBase<T> : IDisposable
    {
        private readonly int _intervalMilliseconds;

        private bool _isRunning = true;

        protected MonitorBase(int intervalMilliseconds)
        {
            _intervalMilliseconds = intervalMilliseconds;
            var thread = new Thread(Run) { IsBackground = true };
            thread.Start();
        }

        public virtual void Dispose()
        {
            _isRunning = false;
        }

        protected abstract T[] Collect();

        protected abstract void Send(T[] items);

        private void Run()
        {
            var stopwatch = new Stopwatch();
            while (_isRunning)
            {
                stopwatch.Restart();

                T[] items = Collect();
                if (items.Length > 0)
                {
                    Send(items);
                }

                long timeout = _intervalMilliseconds - stopwatch.ElapsedMilliseconds;
                Thread.Sleep(timeout > 0 ? (int)timeout : 0);
            }
        }
    }
}