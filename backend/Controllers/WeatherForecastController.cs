using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("generate")]
    public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery] int count, [FromBody] Temperature temp)
    {
        if (count < 0 || temp.min > temp.max || temp == null)
        {
            return BadRequest("Count < 0 || min > max");
        }
        var res = Enumerable.Range(1, count).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(temp.min, temp.max),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return Ok(res);
    }
}

public class Temperature
{
    public int min { get; set; }
    public int max { get; set; }
}