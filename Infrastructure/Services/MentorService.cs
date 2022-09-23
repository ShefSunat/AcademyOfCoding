namespace Infrastructure.Services;
using Npgsql;
using Dapper;
using Domain.Wrapper;
using Infrastructure.DataContext;
using Domain.Models;
public class MentorService
{
    private DataContext _context;
    private Mentor mentor;

    public MentorService(DataContext context) => _context = context;

    public async Task<Response<List<Mentor>>> GetMentors()
    {
        await using var connection = _context.CreateConnection();
        var sql = "select  * from Mentor";
        var result = await connection.QueryAsync<Mentor>(sql);
        return new Response<List<Mentor>>(result.ToList());
    }

    

    public async Task<Response<Mentor>> AddMentor(Mentor Mentor)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql =
                "insert into Mentor (FirstName ,LastName,Email,Phone,Address,City) values (@FirstName, @LastName, @Email,@Phone,@Address,@City) returning id;";
            var result =
                await connection.ExecuteScalarAsync<int>(sql,
                    new { Mentor.FirstName, Mentor.LastName,Mentor.Email,Mentor.Phone,Mentor.Address,Mentor.City });
            Mentor.Id = result;
            return new Response<Mentor>(mentor);
        }
        catch (Exception ex)
        {
            return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
      public async Task<Response<Mentor>> UpdateMentor(Mentor Mentor)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"update Mentor set FirstName = @FirstName, LastName = @LastName, Email = @Email,Phone = @Phone,Address =  @Address, City = @City  where Id = @Id returning Id";
            var response  = await connection.ExecuteScalarAsync<int>(sql, new{Mentor.FirstName, Mentor.LastName, Mentor.Email, Mentor.Phone,Mentor.Address,Mentor.City});
            Mentor.Id = response;
            return new Response<Mentor>(Mentor);
        }
        }
         catch (Exception ex)
        {     
           return new Response<Mentor>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }  
    }
       public async Task<Response<int>> DeleteMentor(int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Mentor where Id = {id}";
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
