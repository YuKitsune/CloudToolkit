namespace CloudToolkit.Email.SendGrid
{
    /// <summary>
    ///     The SendGrid Configuration.
    /// </summary>
    public class SendGridConfiguration
    {
        /// <summary>
        ///     The API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     Gets or sets the email address to send the emails from.
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        ///     Gets or sets the display name to use in any sent emails.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
