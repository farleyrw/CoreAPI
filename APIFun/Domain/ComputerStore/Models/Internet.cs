using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiFun.Base;

namespace APIFun.Domain.ComputerStore.Models
{
	[Table("internet")]
	public class Internet : BaseEntity
	{
		[Key]
		public int Id { get; set; }

		public int ProviderId { get; set; }

		[ForeignKey("ProviderId")]
		public CableCompany CableCompany { get; set; }

		public int Speed { get; set; }

		public string AddUser { get; set; }

		public DateTime AddDate { get; set; }

		public string ModUser { get; set; }

		public DateTime ModDate { get; set; }
	}
}
