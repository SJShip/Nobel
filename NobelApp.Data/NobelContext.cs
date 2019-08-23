using Microsoft.EntityFrameworkCore;
using NobelApp.Domain;
using Microsoft.Extensions.Configuration;
using System;

namespace NobelApp.Data
{
	public class NobelContext: DbContext
	{
		public DbSet<Laureate> Laureates { get; set; }
		public DbSet<Prize> Prizes { get; set; }
		public DbSet<Organization> Organizations { get; set; }

		public string SeedSourceFilePath { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Seed(SeedSourceFilePath);
		}

		public NobelContext(DbContextOptions<NobelContext> options) : base(options)
		{

		}
	}
}
