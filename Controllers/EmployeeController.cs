using System.Runtime.CompilerServices;
using EmployeeApi.Models;
using AutoMapper;
using EmployeeApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using EmployeeApi.DTOs;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeApi.Controllers;


[ApiController]
[Route ("api/[controller]")]
[Authorize(Roles = "Admin,User")]

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

public EmployeeController(IEmployeeRepository repo, IMapper mapper)
{
    _repository = repo;
    _mapper = mapper;
}

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        var employees = await _repository.GetAllAsync();
        var result = _mapper.Map<List<EmployeeReadDto>>(employees);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if(employee == null)
        return NotFound();

        var dto = _mapper.Map<EmployeeReadDto>(employee);
         return Ok(dto);

    }
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto dto)
    {
       var employee = _mapper.Map<Employee>(dto);

        await _repository.AddAsync(employee);

        var result = _mapper.Map<EmployeeReadDto>(employee);

         return CreatedAtAction(
            nameof(GetById),
            new { id = employee.Id },
            result
        );

    }
   [Authorize(Roles = "Admin")]
    [HttpPut ("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody]EmployeeUpdateDto dto)
    {    


         var existing = await _repository.GetByIdAsync(id);
        if( existing == null ) 
           return NotFound();
         
         
         _mapper.Map(dto,existing);
         

        await _repository.UpdateAsync(existing);
        return NoContent();

    }

   [Authorize(Roles = "Admin")]
     [HttpDelete ("{id}")]
     public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if(existing == null)
        return NotFound();

        await _repository.DeleteAsync(id);

        return NoContent();

    }


}