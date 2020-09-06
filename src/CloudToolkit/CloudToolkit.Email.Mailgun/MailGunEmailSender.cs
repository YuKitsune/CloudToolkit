using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

using CloudToolkit.Email.Mailgun.Exceptions;

using RestSharp;
using RestSharp.Authenticators;

namespace CloudToolkit.Email.Mailgun
{
	/// <summary>
	///		The Mailgun implementation of the <see cref="BaseEmailSender"/>.
	/// </summary>
	public class MailGunEmailSender : BaseEmailSender
	{
		/// <summary>
		///		The <see cref="MailgunConfiguration"/>.
		/// </summary>
		private readonly MailgunConfiguration _configuration;

		/// <summary>
		///		Initializes a new instance of the <see cref="MailGunEmailSender"/> class.
		/// </summary>
		/// <param name="configuration">
		///		The <see cref="MailgunConfiguration"/>.
		/// </param>
		public MailGunEmailSender(MailgunConfiguration configuration)
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
			// Setup the Rest Client
			RestClient client = new RestClient
			{
				BaseUrl = new Uri("https://api.mailgun.net/v3"),
				Authenticator = new HttpBasicAuthenticator("api", _configuration.ApiKey)
			};

			// Build the request
			RestRequest request = new RestRequest();
			request.AddParameter("domain", _configuration.Domain, ParameterType.UrlSegment);
			request.Resource = "{domain}/messages";

			// Todo: The example documentation says the "from" address is mailgun@domain, need check if we can override that
			request.AddParameter("from", $"{fromAddress.DisplayName} <mailgun@{_configuration.Domain}>");
			request.AddParameter("to", recipientAddress);
			request.AddParameter("subject", subject);
			request.AddParameter("text", message);
			request.Method = Method.POST;
			IRestResponse response = await client.ExecuteAsync(request, cancellationToken);

			// Todo: Find out if we can get more context from this
			if (!response.IsSuccessful)
			{
				throw new UnsuccessfulEmailException(response, "Failed to send email, unsuccessful response.");
			}
		}
	}
}
