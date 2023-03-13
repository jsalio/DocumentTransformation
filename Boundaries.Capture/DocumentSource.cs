using System;
using System.Collections.Generic;
using System.Text;
using Boundaries.Request;
using Core.Contracts;
using Core.Models;

namespace Boundaries.Capture
{
    public sealed class DocumentSource : IDocumentSource
    {

        private CaptureApiEndPoints _apiEndPoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capureApiEndPoints"></param>
        public DocumentSource(CaptureApiEndPoints capureApiEndPoints)
        {
            _apiEndPoint = capureApiEndPoints;
        }
        IEnumerable<CaptureDocument> IDocumentSource.GetDocumentInQueue()
        {
            var status = new List<int> {7, 92, 27};
            var requestExecutor = new ExecuteRequest();
            var data = requestExecutor.Post<IEnumerable<CaptureDocument>,List<int>>(_apiEndPoint.DataProvider, "/api/documents/document-in-queue", status);
            return data;
        }
    }
}
