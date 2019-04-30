using System.Collections.Generic;

namespace ReservaSalas
{
    public static class BaseUsuario
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario
                {
                    Nome = "fulano", Senha = "123456"
                },
                new Usuario
                {
                    Nome = "fulano1", Senha = "123456"
                },
                new Usuario
                {
                    Nome = "fulano2", Senha = "123456"
                }
            };
        }
    }

    public class Usuario
    {
        public string Nome { get; set; }

        public string Senha { get; set; }
    }
}