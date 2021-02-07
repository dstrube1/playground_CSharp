using System;
using System.Collections.Generic;
using EmersonDataProcessor.model;
using EmersonDataProcessor.model.foo2;

namespace EmersonDataProcessor
{
    public sealed class Foo2DataReader : IFooDataReader
    {
        /*
         * Using singleton pattern because we won't need more than one of these
         */
        private Foo2DataReader()
        {
        }

        private static Foo2DataReader instance = null;

        /*
         * Note: not thread safe, but good enough for now
         */
        public static Foo2DataReader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Foo2DataReader();
                }
                return instance;
            }
        }

        /*
         * Method: Read
         * Parameters: IFoo foo
         * Purpose: parse thru data in foo to extract data for MergedListItem
         * Returns: MergedListItem
         */
        public List<MergedListItem> Read(IFoo foo)
        {
            if (foo == null)
            {
                Console.WriteLine("Null foo");
                return null;
            }

            Foo2 foo2 = (Foo2)foo;
            List<MergedListItem> list = new List<MergedListItem>();
            
            foreach(Device device in foo2.Devices)
            {
                MergedListItem item = new MergedListItem();
                item.CompanyId = foo2.CompanyId;
                item.CompanyName = foo2.Company;
                item.TrackerId = device.DeviceID;
                item.TrackerName = device.Name;
                item.StartDate = device.StartDateTime;
                DateTime first = device.SensorData[0].DateTime;
                DateTime last = device.SensorData[0].DateTime;
                int tempCount = 0;
                double avgTemp;
                double tempSum = 0.0;
                int humidityCount = 0;
                double avgHumidity;
                double humiditySum = 0.0;
                foreach(SensorDatum datum in device.SensorData)
                {
                    if(datum.SensorType.Equals("TEMP"))
                    {
                        tempCount++;
                        tempSum += datum.Value;
                    }
                    else if (datum.SensorType.Equals("HUM"))
                    {
                        humidityCount++;
                        humiditySum += datum.Value;
                    }
                    else
                    {
                        Console.WriteLine("Unexpected sensor type: " + datum.SensorType);
                        return null;
                    }
                    int dateTimeCompareFirst = DateTime.Compare(datum.DateTime, first);
                    if (dateTimeCompareFirst < 0)
                    {
                        first = datum.DateTime;
                    }
                    int dateTimeCompareLast = DateTime.Compare(last, datum.DateTime);
                    if (dateTimeCompareLast < 0)
                    {
                        last = datum.DateTime;
                    }
                }
                avgTemp = tempSum / tempCount;
                avgHumidity = humiditySum / humidityCount;

                item.FirstCrumbDtm = first;
                item.LastCrumbDtm = last;
                item.TempCount = tempCount;
                item.AvgTemp = avgTemp;
                item.HumidityCount = humidityCount;
                item.AvgHumidity = avgHumidity;

                list.Add(item);
            }
            return list;
        }
    }
}
