using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Exceptions
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public sealed class ApiException : Exception
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public ApiException() { }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="message"></param>
        public ApiException(string message) : base(message) { }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public ApiException(string message, Exception exception) : base(message, exception) { }

    }
}
