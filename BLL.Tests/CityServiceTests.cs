using DAL.UnitOfWork;
using DAL.Entities;
using BLL.Services.Impl;
using Moq;
using Xunit;
using System.Collections.Generic;
using DAL.Repositories.Interfaces;
using System.Linq;
using System;

namespace BLL.Tests
{
	public class CityServiceTests
	{
		[Fact]
		public void Ctor_InputNull_ThrowArgumentNullException()
		{
			// Arrange
			IUnitOfWork nullUnitOfWork = null;
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => new CityService(nullUnitOfWork));
		}

		[Fact]
		public void GetStreets_CityFromDAL_CorrectMappingToCityDTO()
		{
			// Arrange
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			var expectedPerson = new Person
			{
				PersonId = 1,
				Surname = "Ivanov",
				Name = "Ivan",
				Gender = 1,
			};
			var expectedCity1 = new City()
			{
				CityId = 1,
				Name = "Kyiv",
				Type = 1,
				People = new List<Person> { expectedPerson }
			};
			var expectedCity2 = new City()
			{
				CityId = 2,
				Name = "Moskwa",
				Type = 2,
				People = new List<Person>()
			};
			var mockDbSet = new Mock<ICityRepository>();
			mockDbSet.Setup(z => z.GetAll()).Returns(new List<City>() { expectedCity1, expectedCity2 });
			mockUnitOfWork
				.Setup(context =>
					context.CityRepository)
				.Returns(mockDbSet.Object);
			var sut = new CityService(mockUnitOfWork.Object);
			// Act
			var s = sut.GetCities();
			// Assert
			Assert.NotEmpty(s);
			Assert.Equal(2, s.Count);
			var actualCity = s.FirstOrDefault(x => x.CityId == expectedCity1.CityId);
			Assert.NotNull(actualCity);
			Assert.True(actualCity.Name == expectedCity1.Name && actualCity.Type == expectedCity1.Type);
			Assert.NotEmpty(actualCity.People);
			Assert.True(actualCity.People.Count() == 1);
			var actualPerson = actualCity.People.First();
			Assert.True(actualPerson.PersonId == expectedPerson.PersonId && actualPerson.Surname == expectedPerson.Surname
				&& actualPerson.Name == expectedPerson.Name && actualPerson.Gender == expectedPerson.Gender);
			actualCity = s.FirstOrDefault(x => x.CityId == expectedCity2.CityId);
			Assert.NotNull(actualCity);
			Assert.True(actualCity.Name == expectedCity2.Name && actualCity.Type == expectedCity2.Type);
			Assert.Empty(actualCity.People);
		}
	}
}
