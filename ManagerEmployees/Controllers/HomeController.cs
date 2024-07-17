using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagerEmployees.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Diagnostics;
using ManagerEmployees.Models.ViewModels;


namespace ManagerEmployees.Controllers
{
    public class HomeController : Controller
    {
        private readonly testemployeeContext _testemployee;
        private readonly IEmployeeService _employeeService;

        public HomeController(testemployeeContext context, IEmployeeService employeeService)
        {
            _testemployee = context;
            _employeeService = employeeService;
        }

        public IActionResult Index(string nameFilter = null)
        {
            IQueryable<Employee> query = _testemployee.Employees.Include(c => c.oStatus);

            // Aplicar el filtro por nombre si se proporciona
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(e => e.Name.Contains(nameFilter) || e.LastName.Contains(nameFilter));
            }

            // Ordenar por fecha de nacimiento
            query = query.OrderBy(e => e.BornDate);

            List<Employee> lista = query.ToList();
            return View(lista);

            //List<Employee> lista = _testemployee.Employees.Include(c => c.oStatus).ToList();
            // return View(lista);

        }

        [HttpGet]
        public IActionResult Empleado_detalle(int idEmployee)
        {
            EmpleadoVM oEmpleadoVM = new EmpleadoVM()
            {
                oEmployee = new Employee(),
                oListaStatus = _testemployee.Statuses.Select(status => new SelectListItem()
                {
                    Text = status.Description,
                    Value = status.IdStatus.ToString()
                }).ToList()
            };

            if (idEmployee != 0)
            { 
                oEmpleadoVM.oEmployee = _testemployee.Employees.Find(idEmployee);
                Console.WriteLine(oEmpleadoVM);
            }

            return View(oEmpleadoVM);
        }

        [HttpPost]
        public IActionResult Empleado_detalle(EmpleadoVM oEmploeadoVM)
        {

            if(oEmploeadoVM.oEmployee.Id == 0)
            {
                if (_testemployee.EmployeeExists(oEmploeadoVM.oEmployee.Rfc))
                {
                
                    ModelState.AddModelError("oEmployee.RFC", "El RFC ya está registrado. Por favor intenta con otro");
                    return View(oEmploeadoVM); 
                }

                _testemployee.Employees.Add(oEmploeadoVM.oEmployee);

            } else
            {
                _testemployee.Employees.Update(oEmploeadoVM.oEmployee);
            }

            _testemployee.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar(int idEmployee)
        {
            Employee oEmployee = _testemployee.Employees.Include(c=>c.oStatus).Where(e =>e.Id == idEmployee).FirstOrDefault();
            
                return View(oEmployee);
        }

        [HttpPost]
        public IActionResult Eliminar(Employee oEmployee)
        {
            _testemployee.Employees.Remove(oEmployee);
            _testemployee.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
