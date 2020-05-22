using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ApiFun.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiFun.Base
{
	public abstract class BaseEntity : IBaseEntity
	{
		[NotMapped]
		public ModelState ModelState { get; set; } = ModelState.Added;
		
		[NotMapped]
		public string OriginalValues
		{
			get
			{
				if(this.originalValues == null) { return string.Empty; }

				string serializedValues = JsonConvert.SerializeObject(this.originalValues);
				
				string protectedValues = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedValues));

				return protectedValues;
			}
			set
			{
				IDictionary<string, object> convertedValues = null;

				if(value != null)
				{
					string decryptedValues = Encoding.UTF8.GetString(Convert.FromBase64String(value));

					convertedValues = JsonConvert.DeserializeObject<IDictionary<string, object>>(decryptedValues);
				}

				this.originalValues = convertedValues;
			}
		}

		private IDictionary<string, object> originalValues { get; set; }

		public void SetOriginalValues(IDictionary<string, object> originalValues) => this.originalValues = originalValues;

		public IDictionary<string, object> GetOriginalValues()
		{
			return this.originalValues;
		}

		public void Remove()
		{
			if (this.IsRemoved)
			{
				return;
			}

			this.ModelState = (this.ModelState == ModelState.Added)
				? ModelState.Detached
				: ModelState.Deleted;
		}

		public bool IsRemoved => this.ModelState == ModelState.Detached || this.ModelState == ModelState.Deleted;

		public void ConvertState(EntityState state)
		{
			this.ModelState = Enum.Parse<ModelState>(state.ToString());
		}
		
		public EntityState ConvertState()
		{
			return Enum.Parse<EntityState>(this.ModelState.ToString());
		}
	}
}