using System;
namespace EmersonDataProcessor.model.foo1
{
    public class Tracker
    {
        public Tracker()
        {
        }
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime ShipmentStartDtm { get; set; }
        public Sensor[] Sensors { get; set; }
    }
}
