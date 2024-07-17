using ManagerEmployees.Models;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
    private readonly testemployeeContext _context;

    public EmployeeService(testemployeeContext context)
    {
        _context = context;
    }

    public IEnumerable<Employee> GetEmployees(string nameFilter = null)
    {
        var query = _context.Employees.Include(c => c.oStatus).AsQueryable();

        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            query = query.Where(e => e.Name.Contains(nameFilter) || e.LastName.Contains(nameFilter));
        }

        return query.OrderBy(e => e.BornDate).ToList();
    }

    public Employee GetEmployeeById(int id)
    {
        return _context.Employees.Include(c => c.oStatus).FirstOrDefault(e => e.Id == id);
    }

    public void AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void UpdateEmployee(Employee employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public void DeleteEmployee(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }

    public bool EmployeeExists(string rfc)
    {
        return _context.Employees.Any(e => e.Rfc == rfc);
    }
}