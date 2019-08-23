using System;

namespace NobelApp.Domain
{
	public class Laureate
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public DateTime BirthDate { get; set; }
		public string BirthCity { get; set; }
		public string BirthCountry { get; set; }
		public SexType Sex { get; set; }
		public Organization Organization { get; set; }
		public DateTime DeathDate { get; set; }
		public string DeathCity { get; set; }
		public string DeathCountry { get; set; }
	}
}
