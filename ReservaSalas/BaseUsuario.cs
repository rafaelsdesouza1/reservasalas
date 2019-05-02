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
                    Nome = "labtrans", Senha = "123"
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