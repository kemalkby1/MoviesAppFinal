﻿using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Directors
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsRetired { get; set; }


        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}