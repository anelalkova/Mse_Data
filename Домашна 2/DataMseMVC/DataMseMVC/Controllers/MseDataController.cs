using DataMseMVC.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DataMseMVC.Controllers
{
    public class MseDataController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public MseDataController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = "https://localhost:7295";
        }

        public async Task<IActionResult> Index()
        {
            var codes = await GetCodes();
            ViewData["Codes"] = new SelectList(codes);

            return View();
        }

        // GET: MseDataController
        public async Task<ActionResult> AllData(int page = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/MseData/GetAllData");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<MseData>>(content);

            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(pagedData);
        }


        private async Task<List<string>> GetCodes()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/MseData/GetCodes");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<string>>(content);

            return data;
        }

        // GET: MseDataController/Details/5
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
        }
    }
}
