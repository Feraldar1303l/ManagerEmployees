using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagerEmployees.Models.ViewModels
{
    public class EmpleadoVM
    {
        public Employee oEmployee { get; set; }
        public List<SelectListItem> oListaStatus { get; set; }
    }
}
