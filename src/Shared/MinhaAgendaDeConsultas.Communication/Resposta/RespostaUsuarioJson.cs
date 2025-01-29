using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Communication.Resposta
{
    public class RespostaUsuarioJson
    {
        public IEnumerable<UsuarioJson> Usuarios { get; set; }

        public static RespostaUsuarioJson FromEntity(IEnumerable<Usuario> usuarios)
        {
            return new RespostaUsuarioJson() { Usuarios = usuarios.Select(x => UsuarioJson.FromEntity(x)) };
        }

        public class UsuarioJson
        {
            public long Id { get; set; }
            //public DateTime DataCriacao { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Senha { get; set; } = string.Empty;


            public static UsuarioJson FromEntity(Usuario usuario)
            {
                return new UsuarioJson()
                {
                    Id = usuario.Id,
                    //DataCriacao = usuario.DataCriacao,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha
                };
            }
        }

    }
}
