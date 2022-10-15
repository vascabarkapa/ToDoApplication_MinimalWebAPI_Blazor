namespace Client.Services.TodoItemService
{
    public interface ITodoItemService
    {
        List<TodoItem> TodoItems { get; set; }

        Task GetTodoItems();

        Task<TodoItem> GetSingleTodoItem(int id);

        Task CreateTodoItem(TodoItem todoItem);

        Task UpdateTodoItem(TodoItem todoItem);

        Task DeleteTodoItem(int id);

        public void BackToList();
    }
}
