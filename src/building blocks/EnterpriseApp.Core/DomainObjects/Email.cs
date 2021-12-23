using System.Text.RegularExpressions;

namespace EnterpriseApp.Core.DomainObjects
{
    public class Email
    {
        public const int EnderecoMaxLength = 254;
        public const int EnderecoMinLength = 5;

        public string EmailAddress { get; private set; }

        // Entity Framework Constructor
        protected Email() { }

        public Email(string emailAddress)
        {
            if (!Validate(emailAddress))
                throw new DomainException("Invalid Email");

            EmailAddress = emailAddress;
        }

        public static bool Validate(string emailAddress)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(emailAddress);
        }
    }
}
