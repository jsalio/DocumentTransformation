using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public sealed class LoggerInfo
    {
        public string AgentId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Message { get; set; }
    }
}
