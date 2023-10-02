using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Web.Controllers.Api;

public sealed class WeatherController : BaseApiController
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetWeather()
    {
        var rng = new Random();
        await Task.Delay(1000);

        int temp = rng.Next(-10, 30);

        string summary = temp switch
        {
            < 0 => "Freezing 🥶",
            < 10 => "Chilly 🥶",
            < 20 => "Cool 😎",
            < 30 => "Warm 🌞",
            _ => "Hot 🥵"
        };

        return Ok(new { Temp = temp, Summary = summary });
    }
}