using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl
{
	public class PersonRepository
		: BaseRepository<Person>, IPersonRepository
	{
		internal PersonRepository(CityStatisticContext context)
			: base(context)
		{
		}
	}
}
