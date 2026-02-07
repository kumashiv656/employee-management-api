

namespace EmployeeApi.DTOs;

public class EmployeeQueryDto
{
    public int Page {get;set;} = 1; 
    public int PageSize {get;set;} = 10;

    public string? SortBy {get;set;}

    public string? SortOrder {get;set;}= "asc";

    public string? Department {get;set;}

    public string? Search {get;set;}

}