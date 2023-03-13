using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;

namespace Core.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<CaptureDocument> GetDocumentInQueue();   
    }
}
