﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University.Services.TeacherAPI.Models.Dto;

public class CourseDto
{
    public int Id { get; set; }
    [Required]
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public int Semester { get; set; }
    public int FacultyId { get; set; }
    public string FacultyName { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public Teacher Teacher { get; set; }
}
