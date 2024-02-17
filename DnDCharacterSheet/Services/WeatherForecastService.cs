using DnDCharacterSheet.Interfaces;

namespace DnDCharacterSheet.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public string SendPing(string message)
        {
            if (message.ToLower() == "error")
            {
                throw new Exception("ERROR");
            }
            else
            {
                return message;
            }

        }
    }
}
