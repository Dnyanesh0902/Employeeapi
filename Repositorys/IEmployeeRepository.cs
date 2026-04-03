using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositorys
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<bool> ExistsAsync(string name, string Designation);
        Task AddAsync(Employee Emp);
        Task UpdateAsync(Employee Employee);    
        Task DeleteAsync(Employee emp);
    }
}
