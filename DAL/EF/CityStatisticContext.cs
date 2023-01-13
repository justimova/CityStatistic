using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
	public class CityStatisticContext : DbContext
	{
		public DbSet<City> Cities { get; set; }
		public DbSet<Person> People { get; set; }

		public CityStatisticContext(DbContextOptions options) : base(options)
		{
		}
	}
}
