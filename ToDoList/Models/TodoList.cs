using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
