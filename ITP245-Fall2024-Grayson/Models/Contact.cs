﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITP245_Fall2024_GraysonModel;

namespace ITP245_Fall2024_Grayson.Models
{
    public class Contact
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Manage { get; set; }
        public List<string> Manages { get; private set; }
        public Contact() 
        {
            using (var entities = new SportsEntities())
            {
                var systemOption = entities.SystemOptions.First();
                Author = systemOption.Name;
            }
            Email = "grayson.trippeer@gmail.com";
            Phone = "(804) 218-6375";
            Description = "Reynolds Community College software management for ITP Course";
            Manage = "This manages both team and individual projects";
            Manages = new List<string>() { "Tasks", "Deadlines", "Progress and", "Other Issues" };
        }
    }
}