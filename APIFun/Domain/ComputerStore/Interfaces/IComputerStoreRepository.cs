using System.Collections.Generic;
using ApiFun.Base.Interfaces;
using APIFun.Domain.ComputerStore.Models;

namespace APIFun.Domain.ComputerStore.Interfaces
{
	public interface IComputerStoreRepository : IRepository
	{
		List<Internet> GetInternets();

		List<CableCompany> GetCableCompanies();
	}
}
