using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP245_Fall2024_Grayson.Models.Sports.cs
{
    public class About
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hometown { get; set; }
        public string Major { get; set; }
        public string Education { get; set; }
        public List<Experience> Experiences { get; set; }

        // Constructor
        public About()
        {
            Name = "Grayson Trippeer";
            Email = "grayson.trippeer@gmail.com";
            Hometown = "Richmond";
            Major = "Information Systems Technology";
            Education = "Mills E. Godwin High School";
            Experiences = new List<Experience>()
        {
            new Experience("Server/Host", "Malabar Indian Cuisine", "2017 - 2019"),
            new Experience("Cashier/Stocker", "Food Lion", "2019 - 2023")
        };
        }
    }

    public class Experience
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Years { get; set; }

        // Constructor
        public Experience(string jobTitle, string company, string years)
        {
            JobTitle = jobTitle;
            Company = company;
            Years = years;
        }
    }
}