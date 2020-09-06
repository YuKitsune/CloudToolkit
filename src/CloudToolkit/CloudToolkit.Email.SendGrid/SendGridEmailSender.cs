using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace CloudToolkit.Email.SendGrid
{
	/// <summary>
	///		The SendGrid implementation of the <see cref="BaseEmailSender"/>.
	/// </summary>
	public class SendGridEmailSender : BaseEmailSender
	{
		/// <summary>
		///		The <see cref="SendGridConfiguration"/>.
		/// </summary>
		private readonly SendGridConfiguration _configuration;

		/// <summary>
		///		Initializes a new instance of the <see cref="SendGridEmailSender"/> class.
		/// </summary>
		/// <param name="configuration">
		///		The <see cref="SendGridConfiguration"/>.
		/// </param>
		public SendGridEmailSender(SendGridConfiguration configuration) 
			: base(configuration.FromAddress, configuration.DisplayName)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}
		
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
		public override async Task SendEmailAsync(
			MailAddress fromAddress,
			string recipientAddress,
			string subject,
			string message,
			CancellationToken cancellationToken = default)
		{
			SendGridClient client = new SendGridClient(_configuration.ApiKey);
			EmailAddress from = new EmailAddress(fromAddress.Address, fromAddress.DisplayName);
			EmailAddress to = new EmailAddress(recipientAddress);

			// Todo: Account for HTML email correctly
			SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
			Response response = await client.SendEmailAsync(msg, cancellationToken);

			// Todo: Find out if we can determine whether or not this failed, and if so, throw an exception with some
			//	more context
		}
	}
}
