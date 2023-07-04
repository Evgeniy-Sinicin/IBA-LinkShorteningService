using System;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LinkShorteningService.IdentityServer.DataAccess.Data.Identity
{
	public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
	{
		public PersistedGrantDbContext CreateDbContext(string[] args)
		{
			var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var builder = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{environmentName}.json", true)
				.AddEnvironmentVariables();

			var config = builder.Build();

			var connstr = config.GetConnectionString("Default");
			var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
			optionsBuilder.UseSqlServer
			(
				connstr,
				sql => sql.MigrationsAssembly(typeof(PersistedGrantDbContextFactory).GetTypeInfo().Assembly.GetName().Name)
			);

			return new PersistedGrantDbContext(optionsBuilder.Options, new OperationalStoreOptions());
		}
	}
}
