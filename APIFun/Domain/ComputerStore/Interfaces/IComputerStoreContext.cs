using ApiFun.Base.Interfaces;
using APIFun.Domain.ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace APIFun.Domain.ComputerStore.Interfaces
{
	public interface IComputerStoreContext : IContext
	{
		DbSet<Internet> Internets { get; set; }

		DbSet<CableCompany> CableCompanies { get; set; }
	}
}
