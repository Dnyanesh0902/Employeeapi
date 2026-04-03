using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _employeeRepository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            var result = await _employeeRepository.GetAll();
            var employees = result.Select(e => new EmployeeDtos
            {
                Name = e.Name,
                Salary = e.Salary,
                Designation = e.Designation,
                Experience = e.Experience
            });
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDtos dtos)
        {
            var result = await _employeeRepository.ExistsAsync(dtos.Name, dtos.Designation);
            if (result)
                return BadRequest("Already Exist");
            var employee = new Employee
            {
                Name = dtos.Name,
                Salary = dtos.Salary,
                Designation = dtos.Designation,
                Experience = dtos.Experience,
            };
            await _employeeRepository.AddAsync(employee);
            var empDto = new EmployeeDtos
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Designation = employee.Designation,
                Experience = employee.Experience
            };
            return Ok(empDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
           var result = await _employeeRepository.GetById(id);
            if (result == null)
                return NotFound();
            var employee = new EmployeeDtos
            {
                Name = result.Name,
                Salary = result.Salary,
                Designation = result.Designation,
                Experience = result.Experience,
            };
            return Ok(employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto Dto)
        {
            var result = await _employeeRepository.GetById(id);
            if (result == null)
                return NotFound();
            result.Name = Dto.Name;
            result.Salary = Dto.Salary;
            result.Designation = Dto.Designation;
            result.Experience = Dto.Experience;
            await _employeeRepository.UpdateAsync(result);
            var employee = new EmployeeDtos
            {
                Name = Dto.Name,
                Salary = Dto.Salary,
                Designation = Dto.Designation,
                Experience = Dto.Experience,
            };

            return Ok(employee);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploee(int id)
        {
            var result = await _employeeRepository.GetById(id);
            if (result == null)
                return NotFound();
            await _employeeRepository.DeleteAsync(result);
            return Ok($"{result.Name} deleted successfully");
        }
    }
}
