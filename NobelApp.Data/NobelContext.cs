using Microsoft.EntityFrameworkCore;
using NobelApp.Domain;
using System;

namespace NobelApp.Data
{
	public class NobelContext: DbContext
	{
		public DbSet<Laureate> Laureates { get; set; }
		public DbSet<Prize> Prizes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = NobelAppData; Trusted_Connection = True; ");

			base.OnConfiguring(optionsBuilder);
		}
	}
}
