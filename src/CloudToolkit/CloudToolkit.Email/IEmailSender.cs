using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace CloudToolkit.Email
{
	/// <summary>
	///		An interface representing a class capable of sending emails.
	/// </summary>
    public interface IEmailSender
	{
		/// <summary>
		///     Sends a plain-text email to the <paramref name="recipientAddress"/>.
		/// </summary>
		/// <param name="recipientAddress">
		///     The email address of the recipient.
		/// </param>
		/// <param name="subject">
		///     The subject of the email.
		/// </param>
		/// <param name="message">
		///     The message to send.
		/// </param>
		void SendEmail(string recipientAddress, string subject, string message);
		
		/// <summary>
		///     Sends a plain-text email to the <paramref name="recipientAddress"/>.
		/// </summary>
		/// <param name="fromAddress">
		///		The <see cref="MailAddress"/> to send the email from.
		/// </param>
		/// <param name="recipientAddress">
		///     The email address of the recipient.
		/// </param>
		/// <param name="subject">
		///     The subject of the email.
		/// </param>
		/// <param name="message">
		///     The message to send.
		/// </param>
		void SendEmail(MailAddress fromAddress, string recipientAddress, string subject, string message);

		/// <summary>
		///     Sends a plain-text email to the <paramref name="recipientAddress"/> as an asynchronous operation.
		/// </summary>
		/// <param name="recipientAddress">
		///     The email address of the recipient.
		/// </param>
		/// <param name="subject">
		///     The subject of the email.
		/// </param>
		/// <param name="message">
		///     The message to send.
		/// </param>
		/// <param name="cancellationToken">
		///		The <see cref="CancellationToken"/> to observe.
		/// </param>
		/// <returns>
		///     The <see cref="Task"/> representing the asynchronous operation.
		/// </returns>
		Task SendEmailAsync(
		    string recipientAddress,
		    string subject,
		    string message,
		    CancellationToken cancellationToken = default);
		
		/// <summary>
		///     Sends a plain-text email to the <paramref name="recipientAddress"/> as an asynchronous operation.
		/// </summary>
		/// <param name="fromAddress">
		///		The <see cref="MailAddress"/> to send the email from.
		/// </param>
		/// <param name="recipientAddress">
		///     The email address of the recipient.
		/// </param>
		/// <param name="subject">
		///     The subject of the email.
		/// </param>
		/// <param name="message">
		///     The message to send.
		/// </param>
		/// <param name="cancellationToken">
		///		The <see cref="CancellationToken"/> to observe.
		/// </param>
		/// <returns>
		///     The <see cref="Task"/> representing the asynchronous operation.
		/// </returns>
		Task SendEmailAsync(
			MailAddress fromAddress,
			string recipientAddress,
			string subject,
			string message,
			CancellationToken cancellationToken = default);
	}
}
