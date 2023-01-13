using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl
{
	public class CityRepository
		: BaseRepository<City>, ICityRepository
	{
		internal CityRepository(CityStatisticContext context)
			: base(context)
		{
		}
	}
}
