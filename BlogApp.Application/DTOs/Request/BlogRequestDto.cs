using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Request
{
    public class BlogRequestDto
    {
         public string Name { get; set; }
        public string Url { get; set; }

        public int AuthorId { get; set; }
    }
}