using Core.Contracts;
using Core.Models.Exceptions;
using Optional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCase.Queue
{
    public sealed class GetQueue
    {
        private readonly IQueueSource _source;

        public GetQueue(IQueueSource source)
        {
            _source = source;
        }

        public Option<Models.Queue, CoreException> Execute()
        {
            try
            {
                var source = _source.GetQueue();
                return Option.Some<Models.Queue, CoreException>(source);
            }
            catch (Exception e)
            {

                return Option.None<Models.Queue, CoreException>((CoreException)e);
            }
        }
    }
}
