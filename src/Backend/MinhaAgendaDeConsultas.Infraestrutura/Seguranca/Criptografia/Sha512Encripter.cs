using Microsoft.Extensions.Configuration;
using MinhaAgendaDeConsultas.Domain.Seguranca.Criptografia;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Criptografia
{
    public class Sha512Encripter : IPasswordEncripter
    {
        private readonly string _chaveAdicional;

        // O valor da chave adicional será passado pelo construtor
        // Injeção de IConfiguration diretamente na classe
        public Sha512Encripter(IConfiguration configuration)
        {
            _chaveAdicional = configuration.GetValue<string>("Settings:Password:chaveAdicional");

            if (string.IsNullOrEmpty(_chaveAdicional))
            {
                throw new InvalidOperationException("A chave adicional não foi configurada.");
            }
        }

        // Método para encriptar uma senha
        public string Encrypt(string password)
        {
            // Cria uma nova senha combinando a senha com a chave adicional
            var newPassword = $"{password}{_chaveAdicional}";

            // Converte a senha combinada em um array de bytes
            var bytes = Encoding.UTF8.GetBytes(newPassword);

            // Gera o hash da senha utilizando SHA512
            var hashBytes = SHA512.HashData(bytes);

            // Converte o array de bytes resultante para uma string hexadecimal
            return StringBytes(hashBytes);
        }

        // Converte um array de bytes para uma string hexadecimal
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
