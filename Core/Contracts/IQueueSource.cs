using Core.Models;

namespace Core.Contracts
{
    /// <summary>
    /// Represents responsability of interact with capture Api's for workflows
    /// </summary>
    public interface IQueueSource
    {
        /// <summary>
        /// Retrieve queue configuration from capture Api's
        /// </summary>
        /// <returns><see cref="Queue"/></returns>
        Queue GetQueue();
        /// <summary>
        /// Save current changes for configurate queue
        /// </summary>
        /// <param name="queue">the <see cref="Queue"/> to affect.</param>
        /// <returns><see cref="string"/> whith confirmation message</returns>
        string Save(Queue queue);
    }

}
