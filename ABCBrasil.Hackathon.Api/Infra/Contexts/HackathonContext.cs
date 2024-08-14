using ABCBrasil.Hackathon.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABCBrasil.Hackathon.Api.Infra.Contexts
{
    public class HackathonContext : DbContext
    {
        public HackathonContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
