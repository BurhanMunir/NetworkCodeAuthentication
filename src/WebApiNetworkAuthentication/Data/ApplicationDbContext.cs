using Microsoft.EntityFrameworkCore;
using WebApiNetworkAuthentication.Models;

namespace WebApiNetworkAuthentication.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<NetworkModel> Networks { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
