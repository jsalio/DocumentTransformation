using Boundaries.Request;
using Core.Contracts;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boundaries.Capture
{
    /// <summary>
    /// Class for interct with capture API's
    /// </summary>
    public sealed class QueueSource : IQueueSource
    {
        private CaptureApiEndPoints _apiEndPoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capureApiEndPoints"></param>
        public QueueSource(CaptureApiEndPoints capureApiEndPoints)
        {
            _apiEndPoint = capureApiEndPoints;
        }

        Queue IQueueSource.GetQueue()
        {
            var requestExecutor = new ExecuteRequest();
            var data = requestExecutor.Get<Queue>(_apiEndPoint.Queue, "/api/queue-configurations?queueConfigurationType=7");
            return data;
        }

        string IQueueSource.Save(Queue queue)
        {
            return "Ok";
        }
    }
}
