using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BLL.Services.Impl
{
	public class CityService : ICityService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CityService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public IList<CityDto> GetCities()
		{
			var entities = _unitOfWork.CityRepository.GetAll();
			var mapperConfig = new MapperConfiguration(cfg => {
				cfg.CreateMap<City, CityDto>().ForMember(s => s.People, c => c.MapFrom(m => m.People));
				cfg.CreateMap<Person, PersonDto>();
				cfg.CreateMap<IEnumerable<Person>, List<PersonDto>>();
			});
			var mapper = mapperConfig.CreateMapper();
			var citiesDto = mapper.Map<IEnumerable<City>, List<CityDto>>(entities);
			return citiesDto;
		}

		public void AddCity(CityDto dto)
		{
			Validate(dto);
			var mapperConfig = new MapperConfiguration(cfg => {
				cfg.CreateMap<CityDto, City>().ForMember(s => s.People, c => c.MapFrom(m => m.People));
				cfg.CreateMap<PersonDto, Person>();
			});
			var mapper = mapperConfig.CreateMapper();
			var entity = mapper.Map<CityDto, City>(dto);
			_unitOfWork.CityRepository.Create(entity);
		}

		private void Validate(CityDto dto)
		{
			if (string.IsNullOrEmpty(dto.Name))
			{
				throw new ArgumentException("Name повинне містити значення!");
			}
		}
	}
}
