using System;
using System.Collections.Generic;
using System.Text;

namespace NobelApp.Data.Domain
{
	public class Organization
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Country { get; set; }

		public List<OrganizationPerson> OrganizationPeople { get; set; }

		public Organization()
		{
			OrganizationPeople = new List<OrganizationPerson>();
		}
	}
}
