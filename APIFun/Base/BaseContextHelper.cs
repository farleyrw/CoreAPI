using System;
using System.Collections.Generic;
using ApiFun.Base.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiFun.Base
{
	public static class BaseContextHelper
	{
		public static void OnObjectMaterialized(object sender, EntityTrackedEventArgs trackedEvent)
		{
			if (trackedEvent.FromQuery && trackedEvent.Entry.Entity is IBaseEntity entity)
			{
				entity.ModelState = ModelState.Unchanged;

				var originalValues = BuildOriginalValues(entity.GetType(), trackedEvent.Entry.OriginalValues);

				entity.SetOriginalValues(originalValues);
			}
		}

		public static void ApplyChanges(ChangeTracker changeTracker)
		{
			foreach (var entry in changeTracker.Entries<IBaseEntity>())
			{
				var modelBase = entry.Entity;

				entry.State = modelBase.ConvertState();

				if (modelBase.ModelState == ModelState.Unchanged)
				{
					ApplyPropertyChanges(entry.OriginalValues, modelBase.GetOriginalValues(), modelBase.GetType());

					modelBase.ConvertState(entry.State);
				}
			}
		}

		public static IDictionary<string, object> BuildOriginalValues(Type entityType, PropertyValues originalValues)
		{
			var result = new Dictionary<string, object>
			{
				["_"] = entityType
			};

			foreach (var property in originalValues.Properties)
			{
				object propertyValue = originalValues[property.Name];

				if (propertyValue is PropertyValues values)
				{
					propertyValue = BuildOriginalValues(entityType.GetProperty(property.Name).GetType(), values);
				}

				result[property.Name] = propertyValue;
			}

			return result;
		}

		public static void ApplyPropertyChanges(PropertyValues values, IDictionary<string, object> originalValues, Type entityType)
		{
			if (values == null || originalValues == null)
			{
				return;
			}
			
			foreach (var item in originalValues)
			{
				if (item.Key.Equals("_")) { continue; }

				object originalValue = item.Value;

				Type propertyType = entityType.GetProperty(item.Key).PropertyType;

				propertyType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

				if (originalValue is IDictionary<string, object> dictionary)
				{
					ApplyPropertyChanges((PropertyValues)values[item.Key], dictionary, propertyType);
				}
				else
				{
					if (originalValue != null)
					{
						object value = propertyType == typeof(Guid) ? Guid.Parse((string)originalValue) : Convert.ChangeType(originalValue, propertyType);

						values[item.Key] = value;
					}
				}
			}
		}
	}
}