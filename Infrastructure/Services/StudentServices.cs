namespace Infrastructure.Services;
using Npgsql;
using Dapper;
using Domain.Wrapper;
using Infrastructure.DataContext;
using Domain.Models;

public class StudentService
{
    private DataContext _context;

    public StudentService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Student>>> GetStudents()
    {
        await using var connection = _context.CreateConnection();
        var sql = "select  * from Student";
        var result = await connection.QueryAsync<Student>(sql);
        return new Response<List<Student>>(result.ToList());
    }

    

    public async Task<Response<Student>> AddStudent(Student Student)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql =
                "insert into Student (FirstName ,LastName,Email,Phone,Address,City) values (@FirstName, @LastName, @Email,@Phone,@Address,@City) returning id;";
            var result =
                await connection.ExecuteScalarAsync<int>(sql,
                    new { Student.FirstName, Student.LastName,Student.Email,Student.Phone,Student.Address,Student.City });
            Student.Id = result;
            return new Response<Student>(Student);
        }
        catch (Exception ex)
        {
            return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
      public async Task<Response<Student>> UpdateStudent(Student Student)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"update Student set FirstName = @FirstName, LastName = @LastName, Email = @Email,Phone = @Phone,Address =  @Address, City = @City  where Id = @Id returning Id";
            var response  = await connection.ExecuteScalarAsync<int>(sql, new{Student.FirstName, Student.LastName, Student.Email, Student.Phone,Student.Address,Student.City});
            Student.Id = response;
            return new Response<Student>(Student);
        }
        }
         catch (Exception ex)
        {     
           return new Response<Student>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }  
    }
       public async Task<Response<int>> DeleteStudent(int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Student where Id = {id}";
            var response  = await connection.ExecuteAsync(sql);
            id = response;
            return new Response<int>(response);
        }
        }
         catch (Exception ex)
        {
           return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}