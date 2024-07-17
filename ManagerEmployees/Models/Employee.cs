using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerEmployees.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(13, ErrorMessage = "El RFC debe tener 13 caracteres.")]
        [RegularExpression(@"^[A-Z]{4}\d{6}[A-Z0-9]{3}$", ErrorMessage = "El RFC no tiene un formato válido.")]
        public string Rfc { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BornDate { get; set; }
        public int IdStatus { get; set; }

        public virtual Status oStatus { get; set; } = null!;
    }
}
