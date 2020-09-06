using System;

using RestSharp;

namespace CloudToolkit.Email.Mailgun.Exceptions
{
    /// <summary>
    ///     The <see cref="Exception"/> indicating that an email has failed to send.
    /// </summary>
    public class UnsuccessfulEmailException : Exception
    {
        /// <summary>
        ///     Gets the <see cref="IRestResponse"/> which indicated there was a failure.
        /// </summary>
        public IRestResponse Response { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnsuccessfulEmailException"/>.
        /// </summary>
        /// <param name="response">
        ///     The <see cref="IRestResponse"/> which indicated there was a failure.
        /// </param>
        /// <param name="message">
        ///     The message.
        /// </param>
        /// <param name="innerException">
        ///     The inner <see cref="Exception"/>.
        /// </param>
        public UnsuccessfulEmailException(IRestResponse response, string message, Exception innerException = null)
	        : base(message, innerException)
        {
	        Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
}
