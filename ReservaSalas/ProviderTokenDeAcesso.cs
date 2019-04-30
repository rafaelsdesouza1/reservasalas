using System;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace ReservaSalas
{
    public class ProviderTokenDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = BaseUsuario.Usuarios().FirstOrDefault(x => x.Nome == context.UserName && x.Senha == context.Password);

            if (usuario == null)
                context.SetError("invalid_grant", "Login não efetuado");

            var IdentidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(IdentidadeUsuario);
        }
    }
}