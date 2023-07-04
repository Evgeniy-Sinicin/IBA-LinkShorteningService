using Microsoft.EntityFrameworkCore;

namespace LinkShorteningService.IdentityServer.DataAccess.Data.Identity
{
	public class UsersContextFactory : DesignTimeDbContextFactoryBase<UsersContext>
	{
		protected override UsersContext CreateNewInstance(DbContextOptions<UsersContext> options)
		{
			return new UsersContext(options);
		}
	}
}
