using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical_Task.Data.DTO
{
    [Table("TTWeatherOfTheDay")]
    public class WeatherOfTheDay
    {
        [Key]
        public long Id { get; set; }

        public DateTime DateUtc { get; set; }
        public string ForecastMessage { get; set; } //Can be deleted

        public double Cloudiness { get; set; }
        public double WindSpeed { get; set; }
        public double Humidity { get; set; }
        public double RainChance { get; set; }
        public double Pressure { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateUtc { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        public long CityId { get; set; }
        public City City { get; set; }

        public virtual ICollection<WeatherTemperatureOfTheDay> WeatherTemperatures { get; set; }
    }
}
