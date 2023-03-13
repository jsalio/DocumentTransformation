using System;
using System.Collections.Generic;
using System.Text;
using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.Documents
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GetListOfDocuments
    {
        private readonly IDocumentSource _source;


        public GetListOfDocuments(IDocumentSource documentSource)
        {
            _source = documentSource;
        }

        public Option<IEnumerable<CaptureDocument>, ApiException> Execute()
        {
            try
            {
                var dataSet = _source.GetDocumentInQueue();
                return Option.Some<IEnumerable<CaptureDocument>, ApiException>(dataSet);
            }
            catch (ApiException e)
            {
                return Option.None<IEnumerable<CaptureDocument>, ApiException>(e);
            }
        }
    }
}
