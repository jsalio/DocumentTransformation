using Boundaries.DocumentTransformation;
using System;
using System.Collections.Generic;
using System.Timers;
using TrasnsformerSvc.Contract;

namespace TrasnsformerSvc.Runners
{
    public sealed class TimerRunner : IServiceRunner
    {
        private readonly Settings _setting;
        public Timer clock;
        private readonly IServiceRunner _serviceRunnerImplementation;

        public event ServiceReachedEventHandler OnServiceRunnerEventHandler;

        public TimerRunner(ServiceSettings serviceSettings, Settings settings)
        {
            _setting = settings;
            if (settings.EnableSecondQueue)
            {
                _serviceRunnerImplementation = new QueueRunner(serviceSettings, settings, true);
                _serviceRunnerImplementation.Build();
                _serviceRunnerImplementation.OnServiceRunnerEventHandler += _serviceRunnerImplementation_OnServiceRunnerEventHandler;
            }
        }

        void IServiceRunner.Build()
        {
            clock = new Timer(BuildInterval(_setting.Interval, _setting.TimeUnit));
            clock.Enabled = true;
            clock.Elapsed += ExecuteClockJob;
            clock.Start();
        }

        void IServiceRunner.StopRunner()
        {
            clock.Stop();
            if (_setting.EnableSecondQueue && _serviceRunnerImplementation != null)
            {
                _serviceRunnerImplementation.StopRunner();
            }
        }

        private async void ExecuteClockJob(object sender, ElapsedEventArgs e)
        {
            OnServiceRunnerEventHandler?.Invoke(this, new ServiceReachedEventArgs
            {
                DateTimeOffset = DateTimeOffset.Now,
                JsonData = $"Clock execute at {DateTimeOffset.Now}",
                Type = QueueReceiverType.Batches
            });
        }

        public long BuildInterval(int value, TimerUnit unit)
        {
            const int milisecondFator = 1000;
            Dictionary<TimerUnit, long> conversionUnit = new Dictionary<TimerUnit, long>();
            conversionUnit.Add(TimerUnit.Seconds, milisecondFator);
            conversionUnit.Add(TimerUnit.Minutes, 60 * milisecondFator);
            conversionUnit.Add(TimerUnit.Hours, 3600 * milisecondFator);
            conversionUnit.Add(TimerUnit.Days, 86400 * milisecondFator);
            conversionUnit.Add(TimerUnit.Week, 604800 * milisecondFator);
            return value * conversionUnit[unit];
        }

        private void _serviceRunnerImplementation_OnServiceRunnerEventHandler(object sender, ServiceReachedEventArgs e)
        {
            OnServiceRunnerEventHandler?.Invoke(this, e);
        }

    }
}
