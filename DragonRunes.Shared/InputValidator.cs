
using System.Text.RegularExpressions;

namespace DragonRunes.Shared
{
    public static class InputValidator
    {
        private const int MaxNameCaracteres = 20;
        private const int MinNameCaracteres = 3;

        private const int MaxPasswordCaracteres = 20;
        private const int MinPasswordCaracteres = 6;

        private const int MaxEmailCaracteres = 50;
        private const int MinEmailCaracteres = 6;

        public static bool IsValidName(this string name)
        {
            if (String.IsNullOrEmpty(name) || 
                (name.Length > MaxNameCaracteres) || 
                    (name.Length < MinNameCaracteres))
            {
                return false;
            }

            if (!Regex.IsMatch(name, "^[a-zA-Z0-9_]+$"))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidPassword(this string pass)
        {
            if (String.IsNullOrEmpty(pass) || 
                (pass.Length > MaxPasswordCaracteres) || 
                (pass.Length < MinPasswordCaracteres))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidEmail(this string email)
        {
            if (String.IsNullOrEmpty(email) || 
                (email.Length > MaxEmailCaracteres) || 
                (email.Length < MaxEmailCaracteres))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);

                if (addr.Address == email && email.Contains('@'))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
