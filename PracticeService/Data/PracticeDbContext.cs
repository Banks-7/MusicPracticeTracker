using Microsoft.EntityFrameworkCore;
using PracticeService.Models;

namespace PracticeService.Data;

public class PracticeDbContext : DbContext
{
    public PracticeDbContext(DbContextOptions<PracticeDbContext> options)
        : base(options)
    {
    }

    public DbSet<PracticeSession> PracticeSessions => Set<PracticeSession>();
}