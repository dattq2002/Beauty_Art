using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class Post
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime InsDate { get; set; }
        public DateTime UpsDate { get; set; }
        public bool Deflag { get; set; }
        public Guid? UserId { get; set; }
        public string? DescriptionContent { get; set; }

        public virtual User? User { get; set; }
    }
}
