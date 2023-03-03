using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Post[] postsToSeed = new Post[6];

            for(int i = 1; i <= 6; i++)
            {
                //Заполняем посты
                postsToSeed[i - 1] = new Post
                {
                    //задаем новые свойства 
                    PostId = i,
                    Date = DateTime.Now,
                    From = $"From AI",
                    To = $"Manager",
                    Text = $"This is task {i} and do it, please.",
                    
                };
            }

            //Говорим что бы EF заполнил это. Мы перейдем в postToseed
            //И это сообщит EF о том, что нужно заполнить 

            modelBuilder.Entity<Post>().HasData(postsToSeed);
        }
    }
}
