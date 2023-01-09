using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Exceptions
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public sealed class StoreException : Exception
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public StoreException() { }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="message"></param>
        public StoreException(string message) : base(message) { }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public StoreException(string message, Exception exception) : base(message, exception) { }

    }
}
