using Microsoft.AspNetCore.Identity;

namespace LinkShorteningService.IdentityServer.DataAccess.Data.Identity
{
	public class AppUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
