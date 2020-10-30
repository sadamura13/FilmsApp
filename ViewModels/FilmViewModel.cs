using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FilmsApp.ViewModels
{
    public class FilmViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string About { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Author { get; set; }
        public string Creator { get; set; }
        [Required]
        public IFormFile Poster { get; set; }
    }
}
