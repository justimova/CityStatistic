using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.EF
{
	public class EFUnitOfWork : IUnitOfWork
	{
		private CityStatisticContext _dbContext;
		private bool disposed = false;

		public EFUnitOfWork(DbContextOptions options)
		{
			_dbContext = new CityStatisticContext(options);
		}

		private ICityRepository _cityRepository;
		public ICityRepository CityRepository => _cityRepository ??= new CityRepository(_dbContext);

		private IPersonRepository _personRepository;
		public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(_dbContext);

		public void Save() => _dbContext.SaveChanges();

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
