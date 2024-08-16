using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Request
{
    public class PostRequestDto
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public DateTime DatePublished { get; set; }
    }
}