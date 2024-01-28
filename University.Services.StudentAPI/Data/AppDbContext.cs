using Microsoft.EntityFrameworkCore;
using University.Services.StudentAPI.Models;

namespace University.Services.StudentAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}
