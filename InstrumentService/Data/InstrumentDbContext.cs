using InstrumentService.Models;
using Microsoft.EntityFrameworkCore;

namespace InstrumentService.Data;

public class InstrumentDbContext : DbContext
{
    public InstrumentDbContext(DbContextOptions<InstrumentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Instrument> Instruments => Set<Instrument>();
}