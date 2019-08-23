using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NobelApp.Data
{
	public class DataRecord
	{
		[Index(0)]
		public int Year { get; set; }
		[Index(1)]
		public string Category { get; set; }
		[Index(2)]
		public string Prize { get; set; }
		[Index(3)]
		public string Motivation { get; set; }
		[Index(4)]
		public string Share { get; set; }
		[Index(5)]
		public int LaureateId { get; set; }
		[Index(6)]
		public string LaureateType { get; set; }
		[Index(7)]
		public string FullName { get; set; }
		[Index(8)]
		public string BirthDate { get; set; }
		[Index(9)]
		public string BirthCity { get; set; }
		[Index(10)]
		public string BirthCountry { get; set; }
		[Index(11)]
		public string Sex { get; set; }
		[Index(12)]
		public string OrganizationName { get; set; }
		[Index(13)]
		public string OrganizationCity { get; set; }
		[Index(14)]
		public string OrganizationCountry { get; set; }
		[Index(15)]
		public string DeathDate { get; set; }
		[Index(16)]
		public string DeathCity { get; set; }
		[Index(17)]
		public string DeathCountry { get; set; }
	}
}
