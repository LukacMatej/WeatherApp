using OpenMeteo;
using System.ComponentModel.DataAnnotations;
namespace WeatherApp.Models

{
    public class Weather
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Time { get; set; }
        public float? Humidity { get; set; }
        public float? TempFeelsLike { get; set; }
        public float? Temp { get; set; }
        public float? WindSpeed { get; set; }
        public float? WindDir { get; set; }
        public string? WeatherIcon { get; set; }
        public Weather()
        {

        }
    }

}
