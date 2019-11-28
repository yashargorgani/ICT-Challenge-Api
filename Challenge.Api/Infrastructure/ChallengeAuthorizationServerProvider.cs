using Challenge.Api.Context;
using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Challenge.Api.Infrastructure
{
    public class ChallengeAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var _db = new ChallengeContext())
            {
                var hashPass = Services.ChallengeHash.ConvertToSHA256(context.Password);
                var user = 
                    _db.Users.FirstOrDefault(x => x.UserName == context.UserName && 
                                             x.Password == hashPass);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim("Email", user.Email));
                context.Validated(identity);
            }
        }
    }
}