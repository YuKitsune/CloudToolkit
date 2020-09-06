namespace CloudToolkit.Email.Smtp
{
    /// <summary>
    ///     The SMTP configuration.
    /// </summary>
    public class SmtpConfiguration
    {
        /// <summary>
        ///     Gets or sets the server address.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        ///     Gets or sets the port number.
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        public string UserName { get; set; }

        // Todo: See if we can convert this to a SecureString

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the email address to send the emails from.
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        ///     Gets or sets the display name to use in any sent emails.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether or not to enable SSL.
        /// </summary>
        public bool EnableSsl { get; set; }
    }
}
