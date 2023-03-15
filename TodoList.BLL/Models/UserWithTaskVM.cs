namespace TodoList.BLL.Models
{
    public class UserWithTaskVM
    {
        public string Fullname { get; set; }

        public IEnumerable<TaskVM> Tasks { get; set; }
    }
}
