using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal sealed class App_2DBContext : DbContext
    {
        public DbSet<Posts_action> Post_action { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Posts_action[] posts = new Posts_action[5];

            for(int i = 1; i <= 5; i++)
            {
                posts[i - 1] = new Posts_action()
                {
                    PostId = i,
                    id_task = 0,
                    Date = DateTime.Now,
                    whom = $"string",
                    Text = "56",
                };
            }

            modelBuilder.Entity<Posts_action>().HasData(posts);
        }
    }
}
