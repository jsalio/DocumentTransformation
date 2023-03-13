using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public sealed class CaptureDocument
    {
        public long BatchId { get; set; }
        public string BatchName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentHandler { get; set; }
        public int Pages { get; set; }
        public DateTimeOffset LastUpdate { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
    }

    public enum DocumentStatus
    {
        /// <summary>
        /// This status is used when a document has been converted to pdf
        /// </summary>
        ConvertedToPdf = 7,
        /// <summary>
        /// This status is used when a document is waiting for be worked of a digitization exception.
        /// </summary>
        PendingToConvertToPdf = 27,
        /// <summary>
        /// This status is used when a document has a error on convert to pdf process
        /// </summary>
        ErrorOnConvertingToPdf = 92,
    }
}
