using System;

namespace BlogApp.Domain.Entities
{
    public class Post
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
       

    }
}