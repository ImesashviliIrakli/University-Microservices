﻿using System.ComponentModel.DataAnnotations;

namespace University.Services.TeacherAPI.Models.Dto;

public class TeacherDto
{
    public Guid TeacherId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string PrivateNumber { get; set; }
    [Required]
    public int YearsOfExperience { get; set; }
}
