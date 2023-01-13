using DAL.Repositories.Interfaces;
using System;

namespace DAL.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		ICityRepository CityRepository { get; }
		IPersonRepository PersonRepository { get; }
		void Save();
	}
}
