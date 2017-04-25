using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlBulkCopy.Models
{
    [Serializable, XmlRoot("jobs")]
    public class JobViewModel
    {
        [XmlElement("job")]
        public List<Job> Job { get; set; }
    }

    public class Job
    {
        [XmlElement("jobref")]
        public int JobRef { get; set; }
        [XmlElement("date")]
        public string Date { get; set; }
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("company")]
        public string Company { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
        [XmlElement("url")]
        public string Url { get; set; }
        [XmlElement("salarymin")]
        public string SalaryMin { get; set; }
        [XmlElement("salarymax")]
        public string SalaryMax { get; set; }
        [XmlElement("benefits")]
        public string Benefits { get; set; }
        [XmlElement("salary")]
        public string Salary { get; set; }
        [XmlElement("jobtype")]
        public string JobType { get; set; }
        [XmlElement("full_part")]
        public string Full_Part { get; set; }
        [XmlElement("salary_per")]
        public string Salary_Per { get; set; }
        [XmlElement("location")]
        public string Location { get; set; }
        [XmlElement("country")]
        public string Country { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("category")]
        public string Category { get; set; }
        [XmlElement("image")]
        public string Image { get; set; }
    }
}
