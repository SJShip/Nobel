using System;
using System.Collections.Generic;
using System.Text;

namespace NobelApp.Domain
{
	public class Prize
	{
		public int Id { get; set; }
		public ushort Year { get; set; }
		public CategoryType Category { get; set; }
		public string Motivation { get; set; }

		public List<Laureate> Laureates { get; set; }

		public Prize() {
			Laureates = new List<Laureate>();
		}
	}
}
