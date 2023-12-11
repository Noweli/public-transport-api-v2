using Microsoft.EntityFrameworkCore;
using PublicTransportApi.Data.Models;

namespace PublicTransportApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Line> Lines { get; init; }
    public DbSet<StopPoint> StopPoints { get; init; }
}