using Core.Models;
using Core.UseCase.Queue;
using System;
using System.Text;

namespace Core.Contracts
{
    public interface IQueueStore
    {
        string Save(Queue queue);
        Queue GetQueue();
    }
}
