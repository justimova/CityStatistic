using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DAL.Tests
{
	class TestCityRepository : BaseRepository<City>
	{
		public TestCityRepository(DbContext context) : base(context)
		{
		}
	}

	public class BaseRepositoryUnitTests
	{

		[Fact]
		public void Create_InputStreetInstance_CalledAddMethodOfDBSetWithCityInstance()
		{
			// Arrange
			DbContextOptions opt = new DbContextOptionsBuilder<CityStatisticContext>().Options;
			var mockContext = new Mock<CityStatisticContext>(opt);
			var mockDbSet = new Mock<DbSet<City>>();
			mockContext
				.Setup(context => context.Set<City>())
				.Returns(mockDbSet.Object);
			var repository = new TestCityRepository(mockContext.Object);
			City expectedCity = new Mock<City>().Object;
			//Act
			repository.Create(expectedCity);
			// Assert
			mockDbSet.Verify(dbSet => dbSet.Add(expectedCity), Times.Once());
		}

		[Fact]
		public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
		{
			// Arrange
			DbContextOptions opt = new DbContextOptionsBuilder<CityStatisticContext>().Options;
			var mockContext = new Mock<CityStatisticContext>(opt);
			var mockDbSet = new Mock<DbSet<City>>();
			mockContext
				.Setup(context => context.Set<City>())
				.Returns(mockDbSet.Object);
			var repository = new TestCityRepository(mockContext.Object);
			City expectedCity = new City() { CityId = 1 };
			mockDbSet.Setup(mock => mock.Find(expectedCity.CityId)).Returns(expectedCity);
			//Act
			repository.Delete(expectedCity.CityId);
			// Assert
			mockDbSet.Verify(dbSet => dbSet.Find(expectedCity.CityId), Times.Once());
			mockDbSet.Verify(dbSet => dbSet.Remove(expectedCity), Times.Once());
		}

		[Fact]
		public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
		{
			// Arrange
			DbContextOptions opt = new DbContextOptionsBuilder<CityStatisticContext>()
				.Options;
			var mockContext = new Mock<CityStatisticContext>(opt);
			var mockDbSet = new Mock<DbSet<City>>();
			mockContext
				.Setup(context => context.Set<City>())
				.Returns(mockDbSet.Object);
			City expectedCity = new City() { CityId = 1 };
			mockDbSet.Setup(mock => mock.Find(expectedCity.CityId)).Returns(expectedCity);
			var repository = new TestCityRepository(mockContext.Object);
			//Act
			var actualStreet = repository.Get(expectedCity.CityId);
			// Assert
			mockDbSet.Verify(dbSet => dbSet.Find(expectedCity.CityId), Times.Once());
			Assert.Equal(expectedCity, actualStreet);
		}

	}

}
