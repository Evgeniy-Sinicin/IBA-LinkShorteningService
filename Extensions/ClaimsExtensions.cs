using System.Linq;
using System.Security.Claims;

namespace LinkShorteningService.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetEmailFromUser(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        }
    }
}
