using System.Collections.Generic;
using System;

namespace BLL.DTO
{
	public class CityDto
	{
		public int CityId { get; set; }
		public string Name { get; set; }
		public int Type { get; set; }
		public IList<PersonDto> People { get; set; } = new List<PersonDto>();
		public int PeopleCount => People.Count;
	}
}
