using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Response
{
    public class PostResponseDto
    {
        
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
        public string BlogName { get; set; }
    }
}