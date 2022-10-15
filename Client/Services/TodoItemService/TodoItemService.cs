using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Services.TodoItemService
{
    public class TodoItemService : ITodoItemService
    {
        // https://localhost:7019/
        string url = "https://localhost:7019";

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public TodoItemService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

        public async Task CreateTodoItem(TodoItem todoItem)
        {
            var result = await _http.PostAsJsonAsync($"{url}/api/todoItem", todoItem);
            var response = await result.Content.ReadFromJsonAsync<List<TodoItem>>();

            if (response != null)
            {
                TodoItems = response;
                _navigationManager.NavigateTo("todoItems");
            }

            //throw new Exception("Unexpected error on adding new Task");
        }

        public async Task UpdateTodoItem(TodoItem todoItem)
        {
            var result = await _http.PutAsJsonAsync($"{url}/api/todoItem/{todoItem.Id}", todoItem);
            var response = await result.Content.ReadFromJsonAsync<List<TodoItem>>();

            if (response != null)
            {
                TodoItems = response;
                _navigationManager.NavigateTo("todoItems");
            }

            //throw new Exception("Unexpected error on adding new Task");
        }

        public async Task DeleteTodoItem(int id)
        {
            var result = await _http.DeleteAsync($"{url}/api/todoItem/{id}");
            var response = await result.Content.ReadFromJsonAsync<List<TodoItem>>();

            if (response != null)
            {
                TodoItems = response;
                _navigationManager.NavigateTo("todoItems");
            }

            //throw new Exception("Unexpected error on adding new Task");
        }

        public async Task<TodoItem> GetSingleTodoItem(int id)
        {
            var response = await _http.GetFromJsonAsync<TodoItem>($"{url}/api/todoItem/{id}");

            if (response != null)
            {
                return response;
            }
            else
                throw new Exception("To Do not found!");
        }

        public async Task GetTodoItems()
        {
            var response = await _http.GetFromJsonAsync<List<TodoItem>>($"{url}/api/todoItem");

            if (response != null)
            {
                TodoItems = response;
            }
        }

        public void BackToList()
        {
            _navigationManager.NavigateTo("todoItems");
        }
    }
}
