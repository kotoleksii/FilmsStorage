using FilmsStorage.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmsStorage.Models
{
    public class FilmAddModel
    {
        [Required]
        public string FilmName { get; set; }
        [Required]
        //TODO: create custom validator to insure Movie range (1895 - Current Year)
        public int ReleaseYear { get; set; }
        [Required]
        public int GenreID { get; set; }
        public int UserID { get; set; }
        [Required]
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FilmDescription { get; set; }
        public List<Genre> Genres { get; set; }
    }
}