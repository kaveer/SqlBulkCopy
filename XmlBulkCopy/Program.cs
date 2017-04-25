using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using XmlBulkCopy.Models;

namespace XmlBulkCopy
{
    class Program
    {
        //test
        static string connectionString = @"Data Source=DESKTOP-AODP2NL\SQL12FULL;Initial Catalog=Staging;Integrated Security=True";

        static void Main(string[] args)
        {
            string[] jobSite = new string[] { "careersincatering.xml", "Chef-Jobs.xml", "Hotel-Jobs.xml", "Justjobs.xml", "Student-Jobs.xml" };

            foreach (var item in jobSite)
            {
                if (item != "Justjobs.xml")
                    continue;

                string filePath = Path.Combine(@"C:\Users\kaveer\Desktop\XmlFeed\FeedLink", item);
                JobViewModel record = new JobViewModel();

                XmlSerializer serializer = new XmlSerializer(typeof(JobViewModel));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    record = (JobViewModel)serializer.Deserialize(fileStream);
                }

                BulkCopyJobs(record.Job);
            }


        }

        private static void BulkCopyJobs(List<Job> job)
        {
            DataTable tmpCvlibrary = ToDataTable<Job>(job);
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
            {
                bulkCopy.DestinationTableName = "dbo.mdCvLibrary";
                try
                {
                    bulkCopy.WriteToServer(tmpCvlibrary);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
