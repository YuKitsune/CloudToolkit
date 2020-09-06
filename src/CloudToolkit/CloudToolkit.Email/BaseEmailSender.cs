using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace CloudToolkit.Email
{
	/// <summary>
	///		The base implementation of the <see cref="IEmailSender"/>.
	/// </summary>
    public abstract class BaseEmailSender : IEmailSender
	{
		/// <summary>
		///		Gets the default "From" address to use when sending emails.
		/// </summary>
		public string DefaultFromAddress { get; }

		/// <summary>
		///		Gets the default display name to use when sending emails.
		/// </summary>
		public string DefaultDisplayName { get; }

		protected BaseEmailSender(string defaultFromAddress, string defaultDisplayName = null)
		{
			if (string.IsNullOrWhiteSpace(defaultFromAddress)) throw new ArgumentNullException(nameof(defaultFromAddress));
			DefaultFromAddress = defaultFromAddress;
			DefaultDisplayName = defaultDisplayName;
		}

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
		public virtual void SendEmail(string recipientAddress, string subject, string message) =>
			SendEmail(new MailAddress(DefaultFromAddress, DefaultDisplayName), recipientAddress, subject, message);

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
		public virtual void SendEmail(
			MailAddress fromAddress,
			string recipientAddress,
			string subject,
			string message) =>
			SendEmailAsync(fromAddress, recipientAddress, subject, message).GetAwaiter().GetResult();

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
		public virtual async Task SendEmailAsync(
			string recipientAddress,
			string subject,
			string message,
			CancellationToken cancellationToken = default) => 
			await SendEmailAsync(
				new MailAddress(DefaultFromAddress, DefaultDisplayName),
				recipientAddress,
				subject,
				message,
				cancellationToken);

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
		public abstract Task SendEmailAsync(
			MailAddress fromAddress,
			string recipientAddress,
			string subject,
			string message,
			CancellationToken cancellationToken = default);
	}
}
