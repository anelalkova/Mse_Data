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

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await this.mseDataRepository.GetAllDataAsync());
        }

   /*     // GET: MseDataController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MseDataController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MseDataController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MseDataController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MseDataController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MseDataController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MseDataController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
