using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Exceptions
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public sealed class CoreException : Exception
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public CoreException() { }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="message"></param>
        public CoreException(string message) : base(message) { }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public CoreException(string message, Exception exception) : base(message, exception) { }

    }
}
