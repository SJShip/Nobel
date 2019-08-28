using System;
using System.Collections.Generic;
using System.Text;

namespace NobelApp.Data.Domain
{
	public class OrganizationPerson
	{
		public int OrganizationId { get; set; }
		public Organization Organization { get; set; }

		public int LaureateId { get; set; }
		public IndividualLaureate Laureate { get; set; }

		public int Year { get; set; }
	}
}
