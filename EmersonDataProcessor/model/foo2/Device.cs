using System;
namespace EmersonDataProcessor.model.foo2
{
    public class Device
    {
        public Device()
        {
        }
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public SensorDatum[] SensorData { get; set; }
    }
}
