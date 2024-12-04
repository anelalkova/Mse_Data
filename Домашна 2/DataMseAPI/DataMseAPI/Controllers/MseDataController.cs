using DataMseAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataMseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MseDataController : Controller
    {
        private readonly ILogger<MseDataController> logger;
        private readonly IMseDataRepository mseDataRepository;

        public MseDataController(ILogger<MseDataController> logger, IMseDataRepository mseDataRepository)
        {
            this.logger = logger;
            this.mseDataRepository= mseDataRepository;
        }

        [HttpGet("GetAllData")]
        public async Task<ActionResult> Get()
        {
            return Ok(await this.mseDataRepository.GetAllDataAsync());
        }

        [HttpGet("GetCodes")]
        public async Task<ActionResult> GetCodes()
        {
            return Ok(await this.mseDataRepository.GetCodesAsync());
        }
        [HttpGet("GetDataByCode")]
        public async Task<ActionResult>GetDataByCode(string code)
        {
            return Ok(await this.mseDataRepository.GetDataByCodeAsync(code));
        }
    }
}
