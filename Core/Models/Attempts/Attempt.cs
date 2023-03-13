using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Attempts
{
    public sealed class Attempt
    {
        public long Id { get; set; }
        public string BatchName { get; set; }   
        public long BatchId { get; set; }
        public long DocumentHandler { get; set; }
        public long DocumentType { get; set; }
        public DateTimeOffset RegistryDate { get; set; }    
        public DateTimeOffset LastUpDate { get; set; }
        public AttemptCaseStatus CaseCaseStatus { get; set; }
        public ISet<AttemptDetail> AttemptDetails { get; set; }
    }

    public sealed class AttemptView
    {
        public long Id { get; set; }
        public string BatchName { get; set; }   
        public long BatchId { get; set; }
        public long DocumentHandler { get; set; }
        public long DocumentType { get; set; }
        public DateTimeOffset RegistryDate { get; set; }
        public int Attempt { get; set; }
    }

    public sealed class AddAttemptRequest
    {
        public long BatchId { get; set; }
        public long DocumentHandler { get; set; }
        public long DocumentType { get; set; }
        public string Message { get; set; }
    }

    public sealed class UpdateCaseAttempt
    {
        public long CaseId { get; set; }
        public long DocumentId { get; set; }
    }

    public sealed class AttemptDetail
    {
        public int  Id { get; set; }
        public long AttemptId { get; set; }
        public DateTimeOffset RegistryDate { get; set; }
        public string ErrorDetails { get; set; }
        public Attempt Attempt { get; set; }
        public AttemptDetailStatus Status { get; set; }
    }   

    public enum AttemptCaseStatus
    {
        Open = 0,
        Lock =2,
        Close =1
    }

    public enum AttemptDetailStatus
    {
        Inactive = 0,
        Active = 1,
        Closed = 2
    }
}
