using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Core.Entities
{
    public class TelemetryLogs
    {
        public int Id { get; set; }
        public string SourceType { get; set; } = string.Empty; // Container, Crane, WeatherStation
        public string SourceId { get; set; } = string.Empty; // Örn: MSKU9081234, Gantry-01
        public string SensorType { get; set; } = string.Empty; // Temperature, WindSpeed
        public double SensorValue { get; set; } // Sensör okuma değeri
        public DateTime LogDate { get; set; }
    }
}
