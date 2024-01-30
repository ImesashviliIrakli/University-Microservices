using Microsoft.EntityFrameworkCore;
using University.Services.TeacherAPI.Models;

namespace University.Services.TeacherAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
}
