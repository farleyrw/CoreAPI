using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiFun.Base;

namespace APIFun.Domain.ComputerStore.Models
{
	[Table("cableCompany")]
	public class CableCompany : BaseEntity
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string AddUser { get; set; }

		public DateTime AddDate { get; set; }

		public string ModUser { get; set; }

		public DateTime ModDate { get; set; }
	}
}
