using System;
using System.Collections.Generic;
using System.Text;

namespace NobelApp.Data.Domain
{
	public class Prize
	{
		public int Id { get; set; }
		public int Year { get; set; }
		public string Motivation { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public List<IndividualLaureate> IndividualLaureates { get; set; }
		public List<OrganizationalLaureate> OrganizationalLaureates { get; set; }

		public Prize()
		{
			IndividualLaureates = new List<IndividualLaureate>();
			OrganizationalLaureates = new List<OrganizationalLaureate>();
		}
	}
}
