using System;
using Core.Contracts;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.Queue
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GetQueue
    {
        private readonly IQueueSource _source;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public GetQueue(IQueueSource source)
        {
            _source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
