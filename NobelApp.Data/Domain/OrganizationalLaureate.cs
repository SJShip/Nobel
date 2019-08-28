using System;
using System.Collections.Generic;
using System.Text;

namespace NobelApp.Data.Domain
{
	public class OrganizationalLaureate
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int PrizeId { get; set; }
		public Prize Prize { get; set; }
	}
}
