using System;
using System.Collections.Generic;

namespace ManagerEmployees.Models
{
    public partial class Status
    {
        public Status()
        {
            Employees = new HashSet<Employee>();
        }

        public int IdStatus { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
