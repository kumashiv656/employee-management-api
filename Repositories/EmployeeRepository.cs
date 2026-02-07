using EmployeeApi.Data;
using EmployeeApi.DTOs;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context= context;
    }

   public async Task<(List<Employee>, int)> GetAllAsync(EmployeeQueryDto query)
{
    var employees = _context.Employees.AsQueryable();

    // Filtering
    if (!string.IsNullOrEmpty(query.Department))
    {
        employees = employees.Where(e => e.Department == query.Department);
    }

    // Sorting
    if (!string.IsNullOrEmpty(query.SortBy))
    {
        if (query.SortBy.ToLower() == "firstname")
        {
            employees = query.SortOrder == "desc"
                ? employees.OrderByDescending(e => e.FirstName)
                : employees.OrderBy(e => e.FirstName);
        }
    }

    var totalCount = await employees.CountAsync();

    // Pagination
    var data = await employees
        .Skip((query.Page - 1) * query.PageSize)
        .Take(query.PageSize)
        .ToListAsync();

    return (data, totalCount);
}

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);

    }
    public async Task AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
         await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
       _context.Employees.Update(employee);
       await _context.SaveChangesAsync();

    }

    public async Task DeleteAsync(int id)
    
    {
        var emp = await _context.Employees.FindAsync(id);
        if(emp != null)
        {
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync( );

        }

    }
}