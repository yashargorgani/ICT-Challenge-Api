using Challenge.Api.Context;
using Challenge.Api.Services;
using Challenge.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Challenge.Api.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        private ChallengeContext db;
        
        public AccountController()
        {
            db = new ChallengeContext();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(db.Users.Any(x => x.UserName == model.UserName || x.Email == model.Email))
            {
                return BadRequest("UserName or Email is repeated");
            }

            db.Users.Add(new Models.User
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                Password = ChallengeHash.ConvertToSHA256(model.Password)
            });

            if(await db.SaveChangesAsync() == 0)
                return InternalServerError();
            //else
            return Ok();
        }
    }
}  