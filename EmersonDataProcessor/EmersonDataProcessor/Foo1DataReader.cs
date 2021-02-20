using System;
using System.Collections.Generic;
using EmersonDataProcessor.model;
using EmersonDataProcessor.model.foo1;

namespace EmersonDataProcessor
{
    public sealed class Foo1DataReader : IFooDataReader
    {
        /*
         * Using singleton pattern because we won't need more than one of these
         */
        private Foo1DataReader()
        {
        }

        private static Foo1DataReader instance = null;

        /*
         * Note: not thread safe, but good enough for now
         */
        public static Foo1DataReader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Foo1DataReader();
                }
                return instance;
            }
        }

        /*
         * Method: Read
         * Parameters: IFoo foo
         * Purpose: parse thru data in foo to extract data for MergedListItems
         * Returns: List of MergedListItems
         */
        public List<MergedListItem> Read(IFoo foo)
        {
            if (foo == null)
            {
                Console.WriteLine("Null foo");
                return null;
            }

            Foo1 foo1 = (Foo1)foo;
            List<MergedListItem> list = new List<MergedListItem>();

            foreach (Tracker tracker in foo1.Trackers)
            {
                foreach (Sensor sensor in tracker.Sensors)
                {
                    MergedListItem item = new MergedListItem();
                    item.CompanyId = foo1.PartnerId;
                    item.CompanyName = foo1.PartnerName;
                    item.TrackerId = tracker.Id;
                    item.TrackerName = tracker.Model;
                    item.StartDate = tracker.ShipmentStartDtm;
                    DateTime first = sensor.Crumbs[0].CreatedDtm;
                    DateTime last = sensor.Crumbs[0].CreatedDtm;

                    if (sensor.Name.Equals("Temperature"))
                    {
                        int tempCount = sensor.Crumbs.Length;
                        double avgTemp;
                        double tempSum = 0.0;

                        foreach (Crumb crumb in sensor.Crumbs)
                        {
                            tempSum += crumb.Value;

                            int dateTimeCompareFirst = DateTime.Compare(crumb.CreatedDtm, first);

                            if (dateTimeCompareFirst < 0)
                            {
                                first = crumb.CreatedDtm;
                            }

                            int dateTimeCompareLast = DateTime.Compare(last, crumb.CreatedDtm);

                            if (dateTimeCompareLast < 0)
                            {
                                last = crumb.CreatedDtm;
                            }
                        }
                        avgTemp = tempSum / tempCount;

                        item.FirstCrumbDtm = first;
                        item.LastCrumbDtm = last;
                        item.TempCount = tempCount;
                        item.AvgTemp = avgTemp;
                    }
                    else if (sensor.Name.Equals("Humidty"))
                    {
                        int humidityCount = sensor.Crumbs.Length;
                        double avgHumidity;
                        double humiditySum = 0.0;

                        foreach (Crumb crumb in sensor.Crumbs)
                        {
                            humiditySum += crumb.Value;
                            int dateTimeCompareFirst = DateTime.Compare(crumb.CreatedDtm, first);

                            if (dateTimeCompareFirst < 0)
                            {
                                first = crumb.CreatedDtm;
                            }

                            int dateTimeCompareLast = DateTime.Compare(last, crumb.CreatedDtm);

                            if (dateTimeCompareLast < 0)
                            {
                                last = crumb.CreatedDtm;
                            }
                        }

                        avgHumidity = humiditySum / humidityCount;

                        item.FirstCrumbDtm = first;
                        item.LastCrumbDtm = last;
                        item.HumidityCount = humidityCount;
                        item.AvgHumidity = avgHumidity;
                    }
                    else
                    {
                        Console.WriteLine("Unexpected sensor name: " + sensor.Name);
                        return null;
                    }

                    list.Add(item);
                }

            }

            return list;
        }

    }
}
