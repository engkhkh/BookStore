using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BookAuthorViewModels
    {
        public int BookId { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Title { get; set; }
        [StringLength(150,MinimumLength =5)]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public List<Author> Authors { get; set; }
        public IFormFile  file { get; set; }

    }
}
