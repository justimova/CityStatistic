using DAL.Entities;

namespace BLL.DTO
{
	public class PersonDto
	{
		public int PersonId { get; set; }
		public string Surname { get; set; }
		public string Name { get; set; }
		public int Gender { get; set; }
		public City City { get; set; }
	}
}
