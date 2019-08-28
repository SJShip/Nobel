using System;
using System.Collections.Generic;
using System.Text;

namespace NobelApp.Data.Domain
{
	public class IndividualLaureate
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public SexType Sex { get; set; }

		public List<OrganizationPerson> Organizations { get; set; }

		public StoredDate BirthDate { get; set; }
		public string BirthCity { get; set; }
		public string BirthCountry { get; set; }

		public StoredDate DeathDate { get; set; }
		public string DeathCity { get; set; }
		public string DeathCountry { get; set; }

		public int PrizeId { get; set; }
		public Prize Prize { get; set; }

		public IndividualLaureate()
		{
			Organizations = new List<OrganizationPerson>();
		}
	}
}
