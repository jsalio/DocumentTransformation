using Core.Enums;
using System;

namespace Core.Models
{
    public sealed class ServiceSettings
    {
        public int Id { get; set; }
        public WorkMode WorkMode { get; set; }
        public bool EnableSecondQueue { get; set; }
        public TimerOptions TimerWorkMode { get; set; }
        public TimerUnit TimeUnit { get; set; }
        public int Interval { get; set; }
        public DateTime startDate { get; set; }
        public string TimeInit { get; set; }
        public string TimeEnd { get; set; }
    }
}
