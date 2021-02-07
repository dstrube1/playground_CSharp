using System;
namespace EmersonDataProcessor.model.foo2
{
    public class SensorDatum
    {
        public SensorDatum()
        {
        }
        public string SensorType { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
