using DataMseAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

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

        [HttpPost("run-exe")]
        public IActionResult RunExe([FromQuery] string request)
        {
            try
            {
                string exePath = @"techincal_analysis.exe";
                var startInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = request,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(startInfo))
                {
                    if (process == null)
                    {
                        return StatusCode(500, "Error starting executable process.");
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    string errorOutput = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(errorOutput))
                    {
                        return StatusCode(500, $"Error executing executable: {errorOutput}");
                    }

                    if (string.IsNullOrEmpty(output))
                    {
                        return StatusCode(204, "No content returned from the executable.");
                    }

                    try
                    {
                        var parsedOutput = ParseOutput(output);
                        return Ok(parsedOutput);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"An error occurred while parsing the output: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private object ParseOutput(string output)
        {
            var lines = output.Split(new[] { "\\r\\n", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var parsed = new
            {
                OverallSignals = lines.Length > 0 ? ParseSignals(lines[0]) : null,
                DaySignals = lines.Length > 1 ? ParseSignals(lines[1]) : null,
                WeekSignals = lines.Length > 2 ? ParseSignals(lines[2]) : null,
                MonthSignals = lines.Length > 3 ? ParseSignals(lines[3]) : null
            };

            return parsed;
        }

        private Dictionary<string, int> ParseSignals(string line)
        {
            return line
                .Trim('{', '}')
                .Split(',')     
                .Select(part => part.Split(':')) 
                .ToDictionary(
                    kvp => kvp[0].Trim('\'', ' '), 
                    kvp => int.Parse(kvp[1])      
                );
        }

        [HttpGet("GetClosePrice")]
        public async Task<IActionResult> GetClosePrice([FromQuery] string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest(new { message = "Code is required." });
            }

            try
            {
                var data = await mseDataRepository.GetClosePrice(code);

                if (data == null || !data.Any())
                {
                    return NotFound(new { message = "No data found for the specified code." });
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }


    }
}
