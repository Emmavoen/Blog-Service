using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Application.DTOs.Request
{
    public class AuthorRequestDto
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}