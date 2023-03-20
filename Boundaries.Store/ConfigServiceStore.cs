using Core.Contracts;
using Core.Models;
using System;
using Core.Enums;

namespace Boundaries.Store
{
    public sealed class ConfigServiceStore : IServiceConfigStore
    {
        ServiceSettings IServiceConfigStore.GetSettings()
        {
            return new ServiceSettings
            {
                WorkMode = WorkMode.Timer,
                EnableSecondQueue = true,
                TimeUnit = TimerUnit.Seconds,
                Interval = 12,
                startDate = DateTime.Now,
                TimerWorkMode = TimerOptions.Stopwatch,
                TimeEnd = "23:59",
                TimeInit = "00:01",
                Id = 0,

            };
        }

        string IServiceConfigStore.Save(ServiceSettings queue)
        {
            return "Ok";
        }
    }
}
