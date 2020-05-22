using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiFun.Base.Interfaces
{
	public interface IBaseEntity
    {
		ModelState ModelState { get; set; }
		string OriginalValues { get; set; }
		void SetOriginalValues(IDictionary<string, object> originalValues);
		IDictionary<string, object> GetOriginalValues();
		void Remove();
		bool IsRemoved { get; }
		void ConvertState(EntityState state);
		EntityState ConvertState();
	}

	public enum ModelState
	{
		Added,
		Deleted,
		Detached,
		Modified,
		Unchanged
	}
}