using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface Logger
    {
       void AddEvent(LogMessage logEntry);
       void ReportInfoEvent(string message);
       void ReportWarningEvent(string message);
       void ReportErrorEvent(string message);
    }

    public sealed class LogMessage
    {
        public string Message { get; set; }
        public DateTimeOffset Date { get; set; }
        public LogLevel SeverityLevel { get; set; }
    }

    public enum LogLevel
    {
        Information=0,
        Warning=1,
        Errror =2
    }

    public enum LoggerType
    {
        Window = 0,
        File = 1,
        DataBase =2
    }
}
