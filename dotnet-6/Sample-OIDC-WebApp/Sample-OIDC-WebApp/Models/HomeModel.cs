using Sample_OIDC_WebApp.Configuration;
using System.Security.Claims;

namespace Sample_OIDC_WebApp.Models
{
    public class HomeModel
    {
        public HomeModel(ClaimsPrincipal user)
        {
            IsLoggedIn = user.Identity?.IsAuthenticated ?? false;
            if (IsLoggedIn)
            {
                UserId = long.Parse(user.FindFirstValue(SecurityConfiguration.LocalSessionIdClaim));
                Claims = user.Claims.ToList();
            }
            else
            {
                UserId = null;
                Claims = new List<Claim>();
            }
        }

        public bool IsLoggedIn { get; }
        public long? UserId { get; }
        public List<Claim> Claims { get; }
    }
}
