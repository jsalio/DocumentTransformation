using Core.Contracts;
using Core.Models;
using System;

namespace Boundaries.Store
{
    public class Class1 : IQueueStore
    {
        Queue IQueueStore.GetQueue()
        {
            throw new NotImplementedException();
        }

        string IQueueStore.Save(Queue queue)
        {
            throw new NotImplementedException();
        }
    }
}
