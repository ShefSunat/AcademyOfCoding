
namespace Domain.Models;
public class Group
{
    public int Id { get; set; }
    public string? GroupName{get;set;}
    public string? GroupDescription {get;set;}
    public int CourseId {get;set;}
     
}