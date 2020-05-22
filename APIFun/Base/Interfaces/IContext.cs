namespace ApiFun.Base.Interfaces
{
	public interface IContext : IDbContext
    {
		void ApplyChanges<T>(T entity) where T : class, IBaseEntity;
		void SyncChangeTracking();
	}
}