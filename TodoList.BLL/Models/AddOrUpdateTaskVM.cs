using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TodoList.DAL.Enums;

namespace TodoList.BLL.Models
{
    public class AddOrUpdateTaskVM
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        [Required, StringLength(50, ErrorMessage = "character limit of 3 and 50 is exceeded", MinimumLength = 3)]
        public string Title { get; set; }
        [Required, StringLength(1000, ErrorMessage = "character limit of 3 and 1000 is exceeded", MinimumLength = 3)]
        public string Description { get; set; }
        public Priority? Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}
