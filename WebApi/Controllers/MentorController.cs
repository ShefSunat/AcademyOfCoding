using Domain.Models;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;





[ApiController]
[Route("[controller]")]
public class MentorController : ControllerBase
{
    private MentorService _MentorService;
    public MentorController(MentorService MentorService)
    {
        _MentorService = MentorService;
    }
  

    [HttpGet("GetMentors")]
    public async Task<Response<List<Mentor>>> GetMentors()
    {
        return await _MentorService.GetMentors();
    }


    [HttpPost("AddMentor")]
    public async Task<Response<Mentor>> AddMentor(Mentor mentor)
    {
        return await _MentorService.AddMentor(mentor);
    }

    [HttpPut("UpdateMentor")]
    public async Task<Response<Mentor>> UpdateMentor(Mentor mentor)
    {
       return await _MentorService.UpdateMentor(mentor);
    }

    [HttpDelete("DeleteMentor")]
    public async Task<Response<int>> DeleteMentor(int id)
    {
        return await _MentorService.DeleteMentor(id);
    }

}