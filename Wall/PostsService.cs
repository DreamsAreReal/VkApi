using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Models;

namespace Wall
{
    public class PostsService : AbstractService
    {

        public PostsService(Core.User user) : base(user)
        {
        }

        public async IAsyncEnumerable<PostModel> GetPosts(long groupId, int offset = 0, int count=1)
        {
            for (int i = 0; i < count; i++)
            {
                string allPosts = await (await Client.PostAsync(Url + $"/club{groupId}?offset={offset}&own=1", null)).Content
                    .ReadAsStringAsync();






                yield return new PostModel() {Name = "Lol"};

            }

        }


        
    }
}