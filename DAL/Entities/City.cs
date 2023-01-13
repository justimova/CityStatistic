using System.Collections.Generic;

namespace DAL.Entities
{
	public class City
	{
		public int CityId { get; set; }
		public string Name { get; set; }
		public int Type { get; set; }
		public IEnumerable<Person> People { get; set; }
	}
}
