using System.Collections.Generic;
using System.Threading.Tasks;
using Authorization;
using AuthorizationTests;
using Core;
using Core.Models;
using NUnit.Framework;
using Wall;

namespace WallTests
{
    public class PostsServiceTests
    {
        [TestCase(-116181796)]
        public void Test(long id)
        {
            var user = new UserDataMock().User;
            new BasicAuthorization(user).Login().Wait();
            List<PostModel> postModels = new List<PostModel>();
            using (var wall = new Wall.PostsService(user))
            {
                var e = wall.GetPostsFromGroup(id, 15).GetAsyncEnumerator();

                try
                {
                    while (e.MoveNextAsync().AsTask().Result) postModels.Add(e.Current);
                }
                finally { e.DisposeAsync().AsTask().Wait(); }
            }

            Assert.Pass();
        }
    }
}