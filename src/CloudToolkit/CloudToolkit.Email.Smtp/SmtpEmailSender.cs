using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using CloudToolkit.Email.Smtp.Exceptions;

namespace CloudToolkit.Email.Smtp
{
	/// <summary>
	///		The SMTP implementation of the <see cref="BaseEmailSender"/>.
	/// </summary>
	public class SmtpEmailSender : BaseEmailSender
	{
		/// <summary>
		///		The <see cref="SmtpConfiguration"/>.
		/// </summary>
	    private readonly SmtpConfiguration _configuration;

		/// <summary>
		///		Initializes a new instance of the <see cref="SmtpEmailSender"/> class.
		/// </summary>
		/// <param name="configuration">
		///		The <see cref="SmtpConfiguration"/>.
		/// </param>
		public SmtpEmailSender(SmtpConfiguration configuration) 
			: base(configuration.FromAddress, configuration.DisplayName)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));
			ValidateConfiguration(configuration);
			_configuration = configuration;
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
			// Build the message
			using MailMessage mailMessage = new MailMessage
			{
				From = fromAddress,
				Subject = subject,
				Body = message,
				IsBodyHtml = false,
			};

			mailMessage.To.Add(recipientAddress);

			// Build the SMTP client
			using SmtpClient smtpClient = new SmtpClient(_configuration.Server)
			{
				Port = _configuration.Port,
				Credentials = new NetworkCredential(_configuration.UserName, _configuration.Password),
				EnableSsl = _configuration.EnableSsl
			};

			await smtpClient.SendMailAsync(mailMessage);
		}

		/// <summary>
		///		Ensures the given <see cref="SmtpConfiguration"/> instance is valid, and throws an
		///		<see cref="InvalidSmtpConfigurationException"/> if the given instance is invalid.
		/// </summary>
		/// <param name="configuration">
		///		The <see cref="SmtpConfiguration"/> to validate.
		/// </param>
		private static void ValidateConfiguration(SmtpConfiguration configuration)
		{
			if (string.IsNullOrWhiteSpace(configuration.Server)) throw new InvalidSmtpConfigurationException(configuration, $"The {nameof(configuration.Server)} cannot be null or whitespace.");
			if (string.IsNullOrWhiteSpace(configuration.UserName)) throw new InvalidSmtpConfigurationException(configuration, $"The {nameof(configuration.UserName)} cannot be null or whitespace.");
			if (string.IsNullOrWhiteSpace(configuration.Password)) throw new InvalidSmtpConfigurationException(configuration, $"The {nameof(configuration.Password)} cannot be null or whitespace.");
			if (configuration.Port > 0 || configuration.Port < 65535) throw new InvalidSmtpConfigurationException(configuration, $"The {nameof(configuration.Port)} must be a valid port number.");
		}
	}
}
