using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal static class PostActionRepository
    {
        internal async static Task<List<Posts_action>> GetPostsActionAsync()
        {
            using (var db = new App_2DBContext())
            {
                return await db.Post_action.ToListAsync();
            }
        }
        internal async static Task<bool> CreatePostActionAsync(Posts_action postToCreateAction)
        {
            using (var db = new App_2DBContext())
            {
                try
                {
                    await db.Post_action.AddAsync(postToCreateAction);
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
