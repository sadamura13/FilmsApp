using FilmsApp.Models;
using System.Collections.Generic;

namespace FilmsApp.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Film> Films { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}