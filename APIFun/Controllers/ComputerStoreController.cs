using System.Collections.Generic;
using System.Threading.Tasks;
using APIFun.Domain.ComputerStore.Interfaces;
using APIFun.Domain.ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIFun.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ComputerStoreController : ControllerBase
	{
		private readonly IComputerStoreRepository computerStoreRepository;

		public ComputerStoreController(IComputerStoreRepository computerStoreRepository)
		{
			this.computerStoreRepository = computerStoreRepository;
		}

		[HttpGet("internets")]
		public List<Internet> GetInternets()
		{
			return this.computerStoreRepository.GetInternets();
		}

		[HttpPost("save/internet")]
		public async Task<Internet> SaveInternet(Internet internet)
		{
			this.computerStoreRepository.ApplyChanges(internet);

			await this.computerStoreRepository.SaveChangesAsync();

			return internet;
		}

		[HttpGet("companies")]
		public List<CableCompany> GetCableCompanies()
		{
			return this.computerStoreRepository.GetCableCompanies();
		}

		[HttpPost("save/company")]
		public async Task<CableCompany> SaveCableCompany(CableCompany cableCompany)
		{
			this.computerStoreRepository.ApplyChanges(cableCompany);

			await this.computerStoreRepository.SaveChangesAsync();

			return cableCompany;
		}
	}
}
