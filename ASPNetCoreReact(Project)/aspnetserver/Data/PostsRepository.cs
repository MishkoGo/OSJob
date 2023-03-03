using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal static class PostsRepository
    {
        //Операция чтения(получаем сообщение(асинх))
        internal async static Task<List<Post>> GetPostsAsync()
        {
            using (var db = new AppDBContext())
            {
                //Возращаем нашу таблицу задач
                return await db.Posts.ToListAsync();
            }
        }

        //Реализация только одной задачи

        internal async static Task<Post> GetPostByIdAsync(int postId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Posts
                    .FirstOrDefaultAsync(post => post.PostId == postId);
            }
        }

        //Реализация добавление задачи
        //Реализум тип bool, потому что мы сообщаем вызывающему это, сработало ли оно или нет
        internal async static Task<bool> CreatePostAsync(Post postToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    //Добавленный пост будет сохранен
                    await db.Posts.AddAsync(postToCreate);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        //Реализация операции обновления

        internal async static Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    
                    db.Posts.Update(postToUpdate);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        //Реализация операции удаления

        internal async static Task<bool> DeletePostAsync(int postId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    
                    Post postToDelete = await GetPostByIdAsync(postId);
                    db.Remove(postToDelete);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
