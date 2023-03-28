using System.Threading.Tasks;

namespace TrasnsformerSvc.Contract
{
    interface IDocumentConverter
    {
        void BuildEngines();
        Task<string> GenerateDocuments(string request);
        Task<string> GenerateDocument(string request);
    }


}
