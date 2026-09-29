using CareerAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerAdvisor.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Career> Careers => Set<Career>();
    public DbSet<CareerSkill> CareerSkills => Set<CareerSkill>();
}
