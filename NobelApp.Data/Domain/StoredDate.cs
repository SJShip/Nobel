using System;

namespace NobelApp.Data.Domain
{
	public class StoredDate
	{
		public int LaureateId { get; set; }

		public DateTime Value { get; set; }

		public byte Accuracy { get; set; }
	}
}
