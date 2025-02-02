using System.Security.Cryptography;
using System.Text;

namespace MinhaAgendaDeConsultas.Application.Services.Criptografia
{
    public class PasswordEncripter
    {
        public string Encrypt(string password)
        {
            var chaveAdicional = "MinhaAgendaDeConsultas";

            var newPassword = $"{password}{chaveAdicional}";

            var bytes = Encoding.UTF8.GetBytes(newPassword);

            var hashBytes = SHA512.HashData(bytes);

            return StringBytes(hashBytes);
        }

        /*Convert um array de bytes para uma string*/
        private static string StringBytes(byte[] bytes)
        {

            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}

