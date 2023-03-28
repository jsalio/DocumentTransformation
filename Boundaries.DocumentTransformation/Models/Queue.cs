namespace Boundaries.DocumentTransformation
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Queue
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Exchange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VirtualHost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PrefetchSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PrefetchCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Heartbeat { get; set; }
    }
}
