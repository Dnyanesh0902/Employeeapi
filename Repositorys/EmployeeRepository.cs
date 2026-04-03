using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Repositorys
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Employee Emp)
        {
            _appDbContext.Employees.Add(Emp);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee emp)
        {
           _appDbContext.Employees.Remove(emp);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name, string Designation)
        {
           return await _appDbContext.Employees.AnyAsync(p => p.Name == name && p.Designation == Designation);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _appDbContext.Employees.FindAsync(id);
        }

        public async Task UpdateAsync(Employee Employee)
        {
            _appDbContext.Employees.Update(Employee);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
