namespace Infrastructure.Services;
using Npgsql;
using Dapper;
using Domain.Wrapper;
using Domain.Models;
using Infrastructure.DataContext;
public class GroupService
{
     private DataContext _context;
     public GroupService (DataContext context)
    {
        _context = context;
    }

     public async Task<Response<List<Group>>> GetAGroups()
    {
        await using var connection = _context.CreateConnection();
        var sql = "select  * from Group";
        var result = await connection.QueryAsync<Group>(sql);
        return new Response<List<Group>>(result.ToList());
    }
     public async Task<Response<Group>> AddGroup(Group group)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql = "insert into Group (GroupName, GroupDescription, CourseId) values (@GroupName, @GroupDescription, @CourseId) returning id;";
            var result = await connection.ExecuteScalarAsync<int>(sql, new { group.GroupName, group.GroupDescription, group.CourseId });
            group.Id = result;
            return new Response<Group>(group);
        }
        catch (Exception ex)
        {
            return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

      public async Task<Response<Group>> UpdateGroup(Group group)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"update Group set GroupName = @GroupName, GroupDescription = @GroupDescription, CourseId = @CourseId  where Id = @Id returning Id";
            var response  = await connection.ExecuteScalarAsync<int>(sql, new{group.GroupName, group.GroupDescription, group.CourseId, group.Id});
            group.Id = response;
            return new Response<Group>(group);
        }
        }
         catch (Exception e)
        {     
           return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }  
       
    }

    public async Task<Response<int>> DeleteGroup(int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"delete from Group where Id = {id}";
            var response  = await connection.ExecuteScalarAsync<int>(sql);
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
