using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date and Time")]
        public DateTime DateAndTime { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        [Display(Name = "Done")]
        public bool isDone { get; set; }
    }
}
