using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public sealed class Workflow
    {
        public int Id { get; set; }
        public int Handle { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActiveQaIndex { get; set; }
        public bool IsActiveQaScan { get; set; }
        public bool IsMultipleIndexingActive { get; set; }
        public bool ConvertToPdf { get; set; }
        public IEnumerable<DocumentConvertSetting> DocumentConvertSettings { get; set; }
    }

    public sealed class WorkflowDocumentTypes
    {
        public long Id { get; set; }
        public string WorkflowName { get; set; }
        public bool Archived { get; set; }
        public IEnumerable<DocumentType> DocumentTypes { get; set; }
    }

    public class DocumentType
    {
        public string DocumentTypeName { get; set; }
        public long DocumentTypeId { get; set; }
    }

    public sealed class DocumentConvertSetting : DocumentType
    {
        public long Id { get; set; }
        public int WorkflowId { get; set; }
        public bool? ConvertPdf { get; set; }
        public bool? SupportOcr { get; set; }

        public Workflow Workflow { get; set; }
    }

    public class WorkflowModel
    {
        public int Id { get; set; }
        public int WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public string Description { get; set; }
        public IEnumerable<DocumentConvertSettingModel> DocumentConvertSettings { get; set; }
    }

    public class DocumentConvertSettingModel
    {
        public long Id { get; set; }
        public int WorkflowId { get; set; }
        public string DocumentTypeName { get; set; }
        public long DocumentTypeId { get; set; }
        public bool? ConvertPdf { get; set; }
        public bool? SupportOcr { get; set; }
    }
}
