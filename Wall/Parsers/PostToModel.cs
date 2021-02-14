using System;
using System.Collections.Generic;
using AngleSharp;
using AngleSharp.Dom;
using Core;
using Core.Exceptions;
using Core.Models;

namespace Wall.Parsers
{
    public class PostToModel
    {
        public PostModel FromHtml(IElement post, string url)
        {
            IElement author = post?.QuerySelector("a.pi_author");

            ParseUtilities parseUtilities = new ParseUtilities();
            long authorId;
            string postId = String.Empty;
            string authorName = String.Empty;
            string text = String.Empty;
            int likes = 0;
            int reposts = 0;
            int comments = 0;
            string views = String.Empty;


            if (author==null)
            {
                throw new ParseException("Can't parse author name");
            }

            authorName = author.InnerHtml;


            if(!long.TryParse(author?
                   .GetAttribute("data-post-id")
                   .Replace("-", String.Empty)
                   .Split('_')[0], out authorId))
            {
                throw new ParseException("Can't parse author id");
            }



            if (author?.GetAttribute("data-post-id") == null)
            {
                throw new ParseException("Can't parse post id");
            }

            postId = author?.GetAttribute("data-post-id");




            if (post?.QuerySelector(".pi_text")!=null)
            {
                text = post.QuerySelector(".pi_text").InnerHtml;
            }

            if (post?.QuerySelector("b.v_like") != null)
            {
                int.TryParse(post?.QuerySelector("b.v_like").InnerHtml, out likes);
            }

            if (post?.QuerySelector("b.v_share") != null)
            {
                int.TryParse(post?.QuerySelector("b.v_like").InnerHtml, out likes);
            }

            if (post?.QuerySelector("b.v_replies") != null)
            {
                int.TryParse(post?.QuerySelector("b.v_like").InnerHtml, out likes);
            }


            if (post?.QuerySelector("b.v_views")!=null)
            {
                views = post.QuerySelector("b.v_views").InnerHtml;
            }


            var postModel = new PostModel
            {
                Id = postId,
                Owner =
                    new OwnerModel
                    {
                        Id = parseUtilities.ToGroupId(authorId), Name = authorName
                    },
                Text = text,
                Stat = new StatModel
                {
                    Likes = likes,
                    Reposts = reposts,
                    Comments = comments,
                    Views = views
                },
                Attachments = new List<string>()
            };
            foreach (var attachment in post.QuerySelectorAll("div.thumbs_map a"))
            {
                postModel.Attachments.Add($"{url}{attachment.GetAttribute("href").Split('?')[0]}");
            }

            return postModel;
        }


    }
}