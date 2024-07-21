using EmployeeManagement.Applictaion.Models;
using EmployeeManagement.Applictaion.Models.WeatherForecast;
using EmployeeManagement.Applictaion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagement.API.Controllers;

[Authorize]
public class WeatherForecastController : ApiController
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(
            ApiResult<IEnumerable<WeatherForecastResponseModel>>.Success(await _weatherForecastService.GetAsync()));
    }
}
