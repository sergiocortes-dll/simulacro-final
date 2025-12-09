namespace EduTrack.Application.Common.DTOs;

public class AcademicProgramDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationInSemesters { get; set; }
    public bool IsActive { get; set; }
}