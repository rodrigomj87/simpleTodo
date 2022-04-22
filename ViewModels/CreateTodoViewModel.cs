using System.ComponentModel.DataAnnotations;

namespace SimpleTodo.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}