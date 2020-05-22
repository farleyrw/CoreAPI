using System;
using System.Threading.Tasks;
using ApiFun.Base.Interfaces;

namespace ApiFun.Base
{
	public abstract class BaseRepository<TContext> : IRepository where TContext : class, IContext
    {
		protected TContext Context { get; set; }

		public BaseRepository(IContext context)
		{
			this.Context = context as TContext;
		}

		public void ApplyChanges<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
		{
			this.Context.ApplyChanges(entity);
		}

		public async Task<int> SaveChangesAsync(bool syncChangeTracking = true)
		{
			var result = await this.Context.SaveChangesAsync();

			if (syncChangeTracking)
			{
				this.Context.SyncChangeTracking();
			}

			return result;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && this.Context != null)
			{
				this.Context.Dispose();
			}
		}
	}
}