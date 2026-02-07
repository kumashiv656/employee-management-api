using EmployeeApi.DTOs;
using EmployeeApi.Models;

namespace EmployeeApi.Repositories;

public interface IEmployeeRepository
{
    Task<(List<Employee>,int )> GetAllAsync(EmployeeQueryDto query);
    Task<Employee?> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
}
