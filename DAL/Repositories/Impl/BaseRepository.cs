using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.Impl
{
	public abstract class BaseRepository<T> : IRepository<T>
		where T : class
	{
		private readonly DbSet<T> _set;
		private readonly DbContext _context;

		public BaseRepository(DbContext context)
		{
			_context = context;
			_set = context.Set<T>();
		}

		public void Create(T item) => _set.Add(item);

		public void Delete(int id)
		{
			var item = Get(id);
			_set.Remove(item);
		}

		public IEnumerable<T> Find(Func<T, bool> predicate) => Find(predicate, 0, 10);
		public IEnumerable<T> Find(Func<T, bool> predicate, int pageNumber, int pageSize) =>
			_set.Where(predicate)
				.Skip(pageSize * pageNumber)
				.Take(pageNumber)
				.ToList();

		public T Get(int id) => _set.Find(id);

		public IEnumerable<T> GetAll() => _set.ToList();

		public void Update(T item) => _context.Entry(item).State = EntityState.Modified;
	}
}
