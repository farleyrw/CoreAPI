using System.Threading.Tasks;
using ApiFun.Base;
using ApiFun.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIFun.Base
{
	public abstract class BaseContext : DbContext
	{
		private string ConnectionName { get; set; }

		public BaseContext(string connectionName) : base()
		{
			this.ConnectionName = connectionName;

			this.ChangeTracker.Tracked += BaseContextHelper.OnObjectMaterialized;
		}

		public BaseContext(DbContextOptions options) : base(options)
		{
			// For testing?
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//base.OnConfiguring(optionsBuilder);

			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(this.ConnectionName);
			}
		}

		public void ApplyChanges<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
		{
			this.Set<TEntity>().Add(entity);

			BaseContextHelper.ApplyChanges(this.ChangeTracker);
		}

		public void SyncChangeTracking()
		{
			foreach (var entry in this.ChangeTracker.Entries<IBaseEntity>())
			{
				entry.Entity.ModelState = ModelState.Unchanged;

				var originalValues = BaseContextHelper.BuildOriginalValues(entry.Entity.GetType(), this.Entry(entry.Entity).OriginalValues);

				entry.Entity.SetOriginalValues(originalValues);
			}
		}

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}
	}
}
