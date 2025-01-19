using DataMseMVC.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
namespace DataMseMVC.Controllers
{
    [Route("MseData")]
    public class MseDataController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        public MseDataController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = "http://api:80";
        }
        public async Task<IActionResult> Index()
        {
            var codes = await GetCodes();
            ViewData["Codes"] = new SelectList(codes);
            return View();
        }
        // GET: MseDataController
        [HttpGet("AllData")]
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
            return View("Data", pagedData);
        }
        [HttpGet("GetCodes")]
        private async Task<List<string>> GetCodes()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/MseData/GetCodes");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<string>>(content);
            return data;
        }
        [HttpGet("GetDataByCode")]
        [Route("MseData/GetDataByCode")]
        public async Task<IActionResult> GetDataByCode([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is required.");
            }
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/MseData/GetDataByCode?code={code}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<MseData>>(content);
            return View("Data", data);
        }
        [HttpGet("GetStockAnalysis")]
        public async Task<IActionResult> GetStockAnalysis([FromQuery] string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                return BadRequest("Stock code is required.");
            }
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/MseData/run-exe?request={Uri.EscapeDataString(request)}", null);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            var result = await response.Content.ReadAsStringAsync();
            var analysisResult = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(result);
            if (analysisResult == null)
            {
                return BadRequest("Failed to retrieve analysis.");
            }
            TempData["StockAnalysis"] = JsonConvert.SerializeObject(analysisResult);
            TempData["StockCode"] = request;
            return RedirectToAction("StockAnalysis");
        }
        [HttpGet("StockAnalysis")]
        public IActionResult StockAnalysis()
        {
            var analysisJson = TempData["StockAnalysis"]?.ToString();
            var stockCode = TempData["StockCode"]?.ToString();
            if (string.IsNullOrEmpty(analysisJson) || string.IsNullOrEmpty(stockCode))
            {
                return RedirectToAction("Index");
            }
            ViewData["StockAnalysis"] = analysisJson;
            ViewData["StockCode"] = stockCode;
            return View();
        }
    }
}