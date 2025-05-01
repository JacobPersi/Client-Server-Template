using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;

namespace BlazorMinimalApp.Client.Services
{
    public class TodoService : IAsyncDisposable
    {
        private readonly HttpClient _httpClient;
        private HubConnection _hubConnection;
        private readonly string _hubUrl;
        private bool _started = false;

        public TodoService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _hubUrl = navigationManager.ToAbsoluteUri("/todohub").ToString();
        }

        public event Action<TodoItem> OnTodoItemAdded;
        public event Action<TodoItem> OnTodoItemUpdated;
        public event Action<int> OnTodoItemDeleted;

        public async Task StartHubConnection()
        {
            if (_started)
                return;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<TodoItem>("TodoItemAdded", (todoItem) =>
            {
                OnTodoItemAdded?.Invoke(todoItem);
            });

            _hubConnection.On<TodoItem>("TodoItemUpdated", (todoItem) =>
            {
                OnTodoItemUpdated?.Invoke(todoItem);
            });

            _hubConnection.On<int>("TodoItemDeleted", (id) =>
            {
                OnTodoItemDeleted?.Invoke(id);
            });

            await _hubConnection.StartAsync();
            _started = true;
        }

        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TodoItem>>("api/todo") ?? new List<TodoItem>();
        }

        public async Task<TodoItem> GetTodoItemAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TodoItem>($"api/todo/{id}");
        }

        public async Task AddTodoItemAsync(TodoItem todoItem)
        {
            await _httpClient.PostAsJsonAsync("api/todo", todoItem);
        }

        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            await _httpClient.PutAsJsonAsync($"api/todo/{todoItem.Id}", todoItem);
        }

        public async Task DeleteTodoItemAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/todo/{id}");
        }

        public async Task UpdateTodoItemViaHubAsync(int id, bool isComplete)
        {
            if (_hubConnection != null)
            {
                await _hubConnection.SendAsync("UpdateTodoItem", id, isComplete);
            }
        }

        public async Task AddTodoItemViaHubAsync(string title)
        {
            if (_hubConnection != null)
            {
                await _hubConnection.SendAsync("AddTodoItem", title);
            }
        }

        public async Task DeleteTodoItemViaHubAsync(int id)
        {
            if (_hubConnection != null)
            {
                await _hubConnection.SendAsync("DeleteTodoItem", id);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
    }
}