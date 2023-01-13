using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
	public interface ICityService
	{
		IList<CityDto> GetCities();
		void AddCity(CityDto dto);
	}
}
