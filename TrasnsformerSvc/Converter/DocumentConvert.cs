using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boundaries.DocumentTransformation;
using Boundaries.PdfEngine;
using TrasnsformerSvc.Contract;

namespace TrasnsformerSvc.Converter
{
    public sealed class DocumentConvert : IDocumentConverter
    {
        private readonly IEnumerable<EngineView> _engines;
        private readonly ApplicationRules _rules;
        private IPdfConverter converter;
        private IPdfConverter ocrConverter;


        public DocumentConvert(IEnumerable<EngineView> engines, ApplicationRules rules)
        {
            _engines = engines;
            _rules = rules;
        }

        void IDocumentConverter.BuildEngines()
        {
            foreach (var engineView in _engines)
            {
                if (engineView.EngineTypeName == EngineTypeName.Aspose)
                {
                    converter = new AsposeDocumentConverter();
                }

                if (engineView.EngineTypeName == EngineTypeName.Iris && engineView.IsDefault)
                {
                    ocrConverter = new IrisDocumentConverter(engineView);
                }
            }
        }

        async Task<string> IDocumentConverter.GenerateDocument(string request)
        {
            try
            {
                return await ocrConverter.GenerateDocument(new List<string> { "C:\\Users\\jrodriguez\\Pictures\\Screenshot 2022-05-09 104728.png" });
            }
            catch (Exception e)
            {
                return await ocrConverter.GenerateDocument(new List<string> { "C:\\Users\\jrodriguez\\Pictures\\Screenshot 2022-05-09 104728.png" });
            }
            // return await converter.GenerateDocument(new List<string> { "C:\\Users\\jrodriguez\\Pictures\\Screenshot 2022-05-09 104728.png" });
        }

        async Task<string> IDocumentConverter.GenerateDocuments(string request)
        {
            return await converter.GenerateDocument(new List<string> { "C:\\Users\\jrodriguez\\Pictures\\Screenshot 2022-05-09 104728.png" });
        }
    }

}
