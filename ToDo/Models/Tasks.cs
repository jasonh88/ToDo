using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class Tasks
    {
        public int ID { get; set; }
        public string Task { get; set; }
        public string Notes { get; set; }
        public DateTime? Due { get; set; }
        public bool Completed { get; set; }

    }
}