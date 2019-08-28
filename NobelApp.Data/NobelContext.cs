using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NobelApp.Data.Domain;
using System;

namespace NobelApp.Data
{
	public class NobelContext: DbContext
	{
		public DbSet<IndividualLaureate> Laureates { get; set; }

		public DbSet<OrganizationalLaureate> OrganizationalLaureates { get; set; }

		public DbSet<Prize> Prizes { get; set; }

		public string SeedSourceFilePath { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrganizationPerson>().HasKey(e => new { e.LaureateId, e.OrganizationId, e.Year});

			modelBuilder.Entity<OrganizationPerson>()
			 .HasOne(op => op.Laureate)
			 .WithMany(l => l.Organizations)
			 .HasForeignKey(sc => sc.LaureateId);

			modelBuilder.Entity<OrganizationPerson>()
			 .HasOne(op => op.Organization)
			 .WithMany(l => l.OrganizationPeople)
			 .HasForeignKey(sc => sc.OrganizationId);





			modelBuilder.Seed(SeedSourceFilePath);
		}

		public NobelContext(DbContextOptions<NobelContext> options) : base(options)
		{

		}
	}
}
