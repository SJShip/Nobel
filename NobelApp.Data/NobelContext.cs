using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NobelApp.Data.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NobelApp.Data
{
	public class NobelContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }

		public DbSet<Organization> Organizations { get; set; }
		public DbSet<IndividualLaureate> IndividualLaureates { get; set; }

		public DbSet<OrganizationalLaureate> OrganizationalLaureates { get; set; }

		public DbSet<Prize> Prizes { get; set; }

		public DbSet<OrganizationPerson> OrganizationPersons { get; set; }

		public static void Seed(NobelContext context, string sourceFilePath)
		{
			StoredDate getDateTime(string value)
			{
				var regex = new Regex(@"^(\d{2})-(\d{2})-(\d{4})$");
				var match = regex.Match(value);
				byte accuracy = 0;
				if (match.Groups.Count == 4)
				{
					int day = Convert.ToInt32(match.Groups[1].Value);
					int month = Convert.ToInt32(match.Groups[2].Value);
					int year = Convert.ToInt32(match.Groups[3].Value);

					if (day == 0) { accuracy++; day++; }
					if (month == 0) { accuracy++; month++; }
					if (year == 0) accuracy++;

					if (accuracy < 2)
					{
						return new StoredDate { Value = new DateTime(year, month, day), Accuracy = accuracy };
					}
				}
				return new StoredDate { Accuracy = 2 };
			};

			using (var reader = new StreamReader(sourceFilePath))
			{
				using (var csv = new CsvReader(reader))
				{
					csv.Configuration.HasHeaderRecord = false;
					csv.Configuration.Delimiter = ",";

					var individualLaureates = new List<IndividualLaureate>();
					var organizationalLaureates = new List<OrganizationalLaureate>();
					var organizations = new List<Organization>();
					var categories = new List<Category>();

					var organizationPersons = new List<OrganizationPerson>();

					var prizes = new List<Prize>();
					Prize prize = null;
					Category category = null;

					IndividualLaureate individualLaureate = null;
					OrganizationalLaureate organizationalLaureate = null;
					Organization organization = null;

					var records = csv.GetRecords<DataRecord>();
					if (records != null)
					{
						foreach (var record in records)
						{
							category = categories.FirstOrDefault(c => c.Name == record.Category);
							if (category == null)
							{
								category = new Category
								{
									Id = categories.Count,
									Name = record.Category
								};
								categories.Add(category);
							}

							prize = prizes.FirstOrDefault(p => p.Year == record.Year &&
								p.Motivation == record.Motivation &&
								p.CategoryId == category.Id);

							if (prize == null)
							{
								prize = new Prize
								{
									Year = (ushort)record.Year,
									Motivation = record.Motivation,
									Category = category
								};
								prizes.Add(prize);
							}

							if (record.LaureateType == "Individual")
							{
								organization = organizations.FirstOrDefault(o => o.Name == record.OrganizationName &&
								o.City == record.OrganizationCity && o.Country == record.OrganizationCountry);
								if (organization == null && !String.IsNullOrEmpty(record.OrganizationName))
								{
									organization = new Organization
									{
										Id = organizations.Count,
										Name = record.OrganizationName,
										City = record.OrganizationCity,
										Country = record.OrganizationCountry
									};
									organizations.Add(organization);
								}

								individualLaureate = individualLaureates.FirstOrDefault(l => l.Id == record.LaureateId);
								if (individualLaureate == null)
								{
									// Laureate
									individualLaureate = new IndividualLaureate
									{
										Id = record.LaureateId,
										FullName = record.FullName,
										Sex = record.Sex == "Male" ? SexType.Male : SexType.Female,
										BirthCity = record.BirthCity,
										BirthCountry = record.BirthCountry,
										BirthDate = getDateTime(record.BirthDate),
										DeathDate = getDateTime(record.DeathDate),
										DeathCity = record.DeathCity,
										DeathCountry = record.DeathCountry
									};
									individualLaureates.Add(individualLaureate);
								}

								if (organization != null)
								{
									organizationPersons.Add(new OrganizationPerson { OrganizationId = organization.Id, LaureateId = individualLaureate.Id, Year = prize.Year });
								}

								prize.IndividualLaureates.Add(individualLaureate);
							}
							else
							{
								organizationalLaureate = organizationalLaureates.FirstOrDefault(l => l.Id == record.LaureateId);
								if (organizationalLaureate == null)
								{
									organizationalLaureate = new OrganizationalLaureate
									{
										Id = record.LaureateId,
										Name = record.FullName,
									};
									organizationalLaureates.Add(organizationalLaureate);
								}

								if (prize.OrganizationalLaureates == null)
								{
									prize.OrganizationalLaureates = new List<OrganizationalLaureate>();
								}
								prize.OrganizationalLaureates.Add(organizationalLaureate);
							}
						}

						context.Categories.AddRange(categories);
						context.Organizations.AddRange(organizations);
						context.Prizes.AddRange(prizes);

						context.IndividualLaureates.AddRange(individualLaureates);
						context.OrganizationalLaureates.AddRange(organizationalLaureates);

						context.SaveChanges();

						context.OrganizationPersons.AddRange(organizationPersons);
						context.SaveChanges();
					}
				}
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedNever();
			modelBuilder.Entity<Organization>().Property(c => c.Id).ValueGeneratedNever();

			modelBuilder.Entity<OrganizationPerson>().HasKey(o => new { o.LaureateId, o.OrganizationId, o.Year});

			modelBuilder.Entity<OrganizationPerson>()
			 .HasOne(op => op.Laureate)
			 .WithMany(l => l.Organizations)
			 .HasForeignKey(sc => sc.LaureateId);

			modelBuilder.Entity<OrganizationPerson>()
			 .HasOne(op => op.Organization)
			 .WithMany(l => l.OrganizationPeople)
			 .HasForeignKey(sc => sc.OrganizationId);

			modelBuilder.Entity<IndividualLaureate>().Property(c => c.Id).ValueGeneratedNever();
			modelBuilder.Entity<IndividualLaureate>().OwnsOne(l => l.BirthDate);
			modelBuilder.Entity<IndividualLaureate>().OwnsOne(l => l.DeathDate);

			modelBuilder.Entity<OrganizationalLaureate>().Property(c => c.Id).ValueGeneratedNever();
		}

		public NobelContext(DbContextOptions<NobelContext> options) : base(options)
		{

		}
	}
}
