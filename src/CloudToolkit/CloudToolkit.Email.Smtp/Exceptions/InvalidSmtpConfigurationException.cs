using System;

namespace CloudToolkit.Email.Smtp.Exceptions
{
    /// <summary>
    ///     The <see cref="Exception"/> indicating that a particular <see cref="SmtpConfiguration"/> instance is not
    ///     valid.
    /// </summary>
    public sealed class InvalidSmtpConfigurationException : Exception
    {
        /// <summary>
        ///     Gets the invalid <see cref="SmtpConfiguration"/>.
        /// </summary>
        public SmtpConfiguration Configuration { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidSmtpConfigurationException"/> class.
        /// </summary>
        /// <param name="configuration">
        ///     The invalid <see cref="SmtpConfiguration"/>.
        /// </param>
        /// <param name="message">
        ///     The message.
        /// </param>
        /// <param name="innerException">
        ///     The inner <see cref="Exception"/>.
        /// </param>
        public InvalidSmtpConfigurationException(SmtpConfiguration configuration, string message,
	        Exception innerException = null) : base(message, innerException)
        {
	        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
    }
}
