using Domain.Models;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;





[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private StudentService _StudentService;
    public StudentController(StudentService StudentService)
    {
        _StudentService = StudentService;
    }
  

    [HttpGet("GetStudents")]
    public async Task<Response<List<Student>>> GetStudents()
    {
        return await _StudentService.GetStudents();
    }


    [HttpPost("AddStudent")]
    public async Task<Response<Student>> AddStudent(Student Student)
    {
        return await _StudentService.AddStudent(Student);
    }

    [HttpPut("UpdateStudent")]
    public async Task<Response<Student>> UpdateStudent(Student Student)
    {
       return await _StudentService.UpdateStudent(Student);
    }

    [HttpDelete("DeleteStudent")]
    public async Task<Response<int>> DeleteStudent(int id)
    {
        return await _StudentService.DeleteStudent(id);
    }

}