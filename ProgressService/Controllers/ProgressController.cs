using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using ProgressService.Models;

namespace ProgressService.Controllers;

[ApiController]
[Route("api/progress")]
public class ProgressController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ProgressController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("PracticeService");
    }

    [HttpGet("{instrumentId:int}")]
    public async Task<IActionResult> GetProgress(int instrumentId)
    {
        try
        {
            using var response = await _httpClient.GetAsync($"api/practice/instrument/{instrumentId}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new
                {
                    message = "PracticeService returned an unsuccessful response."
                });
            }

            var sessions = await response.Content.ReadFromJsonAsync<List<PracticeSessionSummary>>();

            if (sessions == null)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new
                {
                    message = "PracticeService returned an invalid response."
                });
            }

            var totalMinutes = sessions.Sum(session => session.DurationMinutes);

            return Ok(new { instrumentId, totalMinutes });
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                message = "PracticeService is unavailable."
            });
        }
        catch (TaskCanceledException)
        {
            return StatusCode(StatusCodes.Status504GatewayTimeout, new
            {
                message = "PracticeService did not respond in time."
            });
        }
    }
}