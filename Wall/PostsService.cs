using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Core;
using Core.Exceptions;
using Core.Models;
using Wall.Parsers;

namespace Wall
{
    public class PostsService : AbstractService
    {


        public PostsService(Core.User user) : base(user)
        {
        }

        public async IAsyncEnumerable<PostModel> GetPostsFromGroup(long groupId, int count=5, int offset = 0)
        {
            ParseUtilities parseUtilities = new ParseUtilities();
            groupId = parseUtilities.ToPositiveId(groupId);
            int postsCount = 0;
            // string clubPage = await (await Client.GetAsync($"{Url}/club{groupId}")).Content.ReadAsStringAsync();
            // using (var document = new HtmlParser().ParseDocument(clubPage))
            // {
            //     int countPosts;
            //     IElement countPostsHtml = document.QuerySelector("div .slim_header.slim_header_block_top");
            //     if (!int.TryParse(countPostsHtml?.InnerHtml.Split(' ')[0], out countPosts))
            //     {
            //         throw new ParseException("Can't parse posts count");
            //     }
            //
            //     if (count > countPosts)
            //     {
            //         count = countPosts;
            //     }
            // }

            count = (int)Math.Ceiling((decimal) count / 5);

            for (int i = 0; i < count; i++)
            {
                string allPosts = await (await Client.PostAsync(Url + $"/club{groupId}?offset={offset}&own=1", null!)).Content
                    .ReadAsStringAsync();

                using var document = new HtmlParser().ParseDocument(allPosts);
                foreach (var post in document.QuerySelectorAll("div:not(._ads_block_data_w).wall_item"))
                {
                    PostToModel postToModel = new PostToModel();
                    yield return postToModel.FromHtml(post, Url);
                    postsCount++;
                }

                if (postsCount % 5 == 0)
                {
                    offset += 5;
                }
            }

        }




        
    }
}