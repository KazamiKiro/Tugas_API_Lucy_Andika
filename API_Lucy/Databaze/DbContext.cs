using Microsoft.EntityFrameworkCore;
using API_Lucy.Modelz;

namespace API_Lucy.Databaze
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Pegawai> Pegawais { get; set; }

    }
}