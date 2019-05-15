using System.Collections.Generic;

namespace NewsApp.Models
{
    public class Article : Entity
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}