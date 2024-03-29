﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibraryApp.Data.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
