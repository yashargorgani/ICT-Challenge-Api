using Challenge.Api.Context;
using Challenge.Api.Services;
using System.Data.Entity;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Challenge.Api.Infrastructure
{
    public class ChallengePrincipalProvider
    {
        ChallengeContext db;

        public ChallengePrincipalProvider()
        {
            db = new ChallengeContext();
        }

        public async Task<IPrincipal> CreatePrincipals(string userName, string passWord)
        {
            var pass = ChallengeHash.ConvertToSHA256(passWord);
            var user = await db.Users
                .FirstOrDefaultAsync(x => x.UserName == userName && x.Password == pass);
    
            if (user == null) return null;
            
            var identity = new GenericIdentity(user.UserName);

            IPrincipal principal = new GenericPrincipal(identity, new[] { "User" });

            return principal;
        }
    }
}