using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ApiFun.Base.Interfaces
{
	public interface IDbContext : IDisposable
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		Task<int> SaveChangesAsync();

		// TODO: SqlQuery

		DatabaseFacade Database { get; }
		ChangeTracker ChangeTracker { get; }
	}
}