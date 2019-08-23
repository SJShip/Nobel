using CsvHelper;
using Microsoft.EntityFrameworkCore;
using NobelApp.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NobelApp.Data
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder, string sourceFilePath)
		{
			using (var reader = new StreamReader(sourceFilePath))
			{
				using (var csv = new CsvReader(reader))
				{
					csv.Configuration.HasHeaderRecord = false;
					csv.Configuration.Delimiter = ",";
					var organizations = new List<Organization>();
					var laureates = new List<Laureate>();
					var prizes = new List<Prize>();

					var records = csv.GetRecords<DataRecord>();
					if (records != null)
					{
						foreach (var record in records)
						{

							// Organization 
							var currentOrganization = organizations.FirstOrDefault(o => o.Name == record.OrganizationName && o.Country == record.OrganizationCountry && o.City == record.OrganizationCity);
							if (currentOrganization == null)
							{
								currentOrganization = new Organization
								{
									Id = organizations.Count + 1,
									Name = record.OrganizationName,
									City = record.OrganizationCity,
									Country = record.OrganizationCountry
								};

								organizations.Add(currentOrganization);
							}

							// Laureate
							var laureate = new Laureate
							{
								Id = record.LaureateId,
								FullName = record.FullName,
								Sex = record.Sex == "Male" ? SexType.Male : SexType.Female,
								BirthCity = record.BirthCity,
								BirthCountry = record.BirthCountry,
								BirthDate = DateTime.Parse(record.BirthDate, System.Globalization.CultureInfo.InvariantCulture),
								DeathDate = DateTime.Parse(record.DeathDate, System.Globalization.CultureInfo.InvariantCulture),
								DeathCity = record.DeathCity,
								DeathCountry = record.DeathCountry,
								Organization = currentOrganization
							};
							laureates.Add(laureate);

							// Prize
							var prize = prizes.FirstOrDefault(p => p.Year == record.Year &&
								p.Motivation == record.Motivation &&
								p.Category == (CategoryType)Enum.Parse(typeof(CategoryType), record.Category));

							if (prize == null)
							{
								prizes.Add(new Prize
								{
									Id = prizes.Count + 1,
									Year = (ushort)record.Year,
									Motivation = record.Motivation,
									Category = (CategoryType)Enum.Parse(typeof(CategoryType), record.Category),
									Laureates = new List<Laureate>() { laureate }
								});
							}
							else {
								if (prize.Laureates == null)
								{
									prize.Laureates = new List<Laureate>();
								}
								prize.Laureates.Add(laureate);
							}
						}

						modelBuilder.Entity<Organization>().HasData(organizations);
						modelBuilder.Entity<Laureate>().HasData(laureates);
						modelBuilder.Entity<Prize>().HasData(laureates);
					}
				}
			}
		}
	}
}