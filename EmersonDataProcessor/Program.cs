using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
using EmersonDataProcessor.model;
using EmersonDataProcessor.model.foo1;
using EmersonDataProcessor.model.foo2;
using System.Collections.Generic;

namespace EmersonDataProcessor
{
    public class Program
    {
        public static List<MergedListItem> list;

        public static void Main(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("Null args");
                throw new ArgumentException("Null args");
            }

            /* Process foo 1 */
            if (!ProcessFoo(args, 1))
            {
                Console.WriteLine("Error processing foo type 1");
                return;
            }

            /* Process foo 2 */
            if (!ProcessFoo(args, 2))
            {
                Console.WriteLine("Error processing foo type 2");
                return;
            }
            //Console.WriteLine("Length of args: " + args.Length);
            //Console.WriteLine("Current dir: " + Directory.GetCurrentDirectory());

        }

        public static bool ProcessFoo(string[] args, int fooType)
        {
            if(args == null)
            {
                Console.WriteLine("Null args");
                return false;
            }
            if (fooType != 1 && fooType != 2)
            {
                Console.WriteLine("Invalid fooType: " + fooType);
                return false;
            }

            string fileContents;
            IFoo foo;
            list = new List<MergedListItem>();
            /*
             * Give user option to specify path to foo data files at command line
             * TODO: Give user option to specify which file (and therefore optionally only one file) with flags like -foo1=... and -foo2=...
            */
            if (args.Length == 2)
            {
                var fooPath = fooType == 1 ? args[0] : args[1];
                
                if (!File.Exists(fooPath))
                {
                    Console.WriteLine("File not exists:" + fooPath);
                    return false;
                }
                else
                {
                    using (Stream stream = new FileStream(fooPath, FileMode.Open, FileAccess.Read))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        fileContents = reader.ReadToEnd();
                    }
                    //Console.WriteLine("Contents of TrackerDataFoo"+ fooType + ": ");
                    //Console.WriteLine(fileContents);
                }
            }
            else
            {
                const string FOO1_RESOURCE_PATH = "EmersonDataProcessor.data.TrackerDataFoo1.json";
                const string FOO2_RESOURCE_PATH = "EmersonDataProcessor.data.TrackerDataFoo2.json";

                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(fooType == 1 ? FOO1_RESOURCE_PATH : FOO2_RESOURCE_PATH))

                using (StreamReader reader = new StreamReader(stream))
                {
                    fileContents = reader.ReadToEnd();
                }

            }
            //Console.WriteLine("Contents of TrackerDataFoo"+ fooType + ": ");
            //Console.WriteLine(fileContents);
            JObject jobject = JObject.Parse(fileContents);

            //Console.WriteLine(jobject);

            /*
             * Trying this:
             *      foo = fooType == 1 ? jobject.ToObject<Foo1>() : jobject.ToObject<Foo2>();
             * leads to this error:
             *      Feature 'target-typed conditional expression' is not available in C# 8.0. Please use language version 9.0 or greater.
             */

            IFooDataReader dataReader;

            if (fooType == 1)
            {
                foo = jobject.ToObject<Foo1>();
                dataReader = Foo1DataReader.Instance;
            }
            else
            {
                foo = jobject.ToObject<Foo2>();
                dataReader = Foo2DataReader.Instance;
            }

            List<MergedListItem> tempList = dataReader.Read(foo);
            Console.WriteLine("Got " + tempList.Count + " items to add to list from foo " + fooType);
            foreach (MergedListItem item in tempList)
            {
                list.Add(item);
            }
            

            Console.WriteLine("Successfully processed foo " + fooType);
            return true;
        }

        
    }
}
