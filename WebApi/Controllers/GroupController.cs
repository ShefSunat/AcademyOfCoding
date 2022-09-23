namespace WebApi.Controllers;
using Domain.Models;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    private GroupService _GroupService;
    public GroupController(GroupService GroupService)
    {
        _GroupService = GroupService;
    }
  

    [HttpGet("GetGroups")]
    public async Task<Response<List<Group>>> GetGroups()
    {
        return await _GroupService.GetAGroups();
    }


    [HttpPost("AddGroup")]
    public async Task<Response<Group>> AddGroup(Group Group)
    {
        return await _GroupService.AddGroup(Group);
    }

    [HttpPut("UpdateGroup")]
    public async Task<Response<Group>> UpdateGroup(Group Group)
    {
       return await _GroupService.UpdateGroup(Group);
    }

    [HttpDelete("DeleteGroup")]
    public async Task<Response<int>> DeleteGroup(int id)
    {
        return await _GroupService.DeleteGroup(id);
    }

}