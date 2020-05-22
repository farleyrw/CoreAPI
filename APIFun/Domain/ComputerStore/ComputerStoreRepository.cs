using System.Collections.Generic;
using System.Linq;
using ApiFun.Base;
using APIFun.Domain.ComputerStore.Interfaces;
using APIFun.Domain.ComputerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace APIFun.Domain.ComputerStore
{
	public class ComputerStoreRepository : BaseRepository<IComputerStoreContext>, IComputerStoreRepository
	{
		public ComputerStoreRepository(IComputerStoreContext computerStoreContext) : base(computerStoreContext) { }

		public List<Internet> GetInternets()
		{
			return this.Context.Internets.Include(i => i.CableCompany).ToList();
		}

		public List<CableCompany> GetCableCompanies()
		{
			return this.Context.CableCompanies.ToList();
		}

	}
}
