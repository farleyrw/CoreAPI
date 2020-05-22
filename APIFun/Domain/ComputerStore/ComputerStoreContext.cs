using APIFun.Base;
using APIFun.Domain.ComputerStore.Interfaces;
using APIFun.Domain.ComputerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APIFun.Domain.ComputerStore
{
	public class ComputerStoreContext : BaseContext, IComputerStoreContext
	{
		public ComputerStoreContext(IConfiguration config) : base(config.GetConnectionString("LocalDb"))
		{
		}

		public DbSet<Internet> Internets { get; set; }

		public DbSet<CableCompany> CableCompanies { get; set; }
	}
}
