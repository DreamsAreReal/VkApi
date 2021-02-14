using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class PostModel
    {
        public string Id { get; set; }

        public OwnerModel Owner { get; set; }

        public string Text { get; set; }

        public List<string> Attachments { get; set; }

        public StatModel Stat { get; set; }
        public DateTime Date { get; set; }

    }
}