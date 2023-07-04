using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkShorteningService.IdentityServer.DataAccess.Data.Identity
{
	public class UsersContext : IdentityDbContext<AppUser>
	{
		public UsersContext(DbContextOptions<UsersContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IdentityRole>().HasData(
				new IdentityRole
				{
					Name = Constants.Roles.User,
					NormalizedName = Constants.Roles.User.ToUpper()
				}
			);
		}
	}
}
