// IEmployeeService.cs
using ManagerEmployees.Models;
using Microsoft.EntityFrameworkCore;

public interface IEmployeeService
{
    IEnumerable<Employee> GetEmployees(string nameFilter = null);
    Employee GetEmployeeById(int id);
    void AddEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int id);
    bool EmployeeExists(string rfc);
}

