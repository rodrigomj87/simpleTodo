using System;

namespace SimpleTodo.Models
{
    public class Todo
    {
        public int todoId { get; set; }
        public string todoTitle { get; set; }
        public bool todoDone { get; set; }
        public DateTime todoDate { get; set; } = DateTime.Now;
    }
}