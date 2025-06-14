namespace blazorchat3.Data;

using Timer = System.Threading.Timer;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    private readonly Timer _timer;
    private WeatherForecast[] _forecasts;
    public event Action? OnForecastChanged;
    
    public WeatherForecastService()
    {
        _forecasts = GenerateForecasts(DateOnly.FromDateTime(DateTime.Now));
        // Aktualizace každých 2 sekund
        _timer = new Timer(_ => UpdateForecasts(), null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
    }
    
    private void UpdateForecasts()
    {
        _forecasts = GenerateForecasts(DateOnly.FromDateTime(DateTime.Now));
        OnForecastChanged?.Invoke();
    }
    
    private WeatherForecast[] GenerateForecasts(DateOnly startDate)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }

    public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
    {
        return Task.FromResult(_forecasts);
    }
}
