using EmployeeManagement.Applictaion.Models.WeatherForecast;

namespace EmployeeManagement.Applictaion.Services;

public interface IWeatherForecastService
{
    public Task<IEnumerable<WeatherForecastResponseModel>> GetAsync();
}
