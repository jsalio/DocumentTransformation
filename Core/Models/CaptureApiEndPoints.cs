using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public sealed class CaptureApiEndPoints
    {
        public string Queue { get; set; }
        public string Workflow { get; set; }
        public string DataProvider { get; set; }
        public string XApiKeyValue { get; set; }
        public string ApiKeyName { get; set; }  
    }
}
