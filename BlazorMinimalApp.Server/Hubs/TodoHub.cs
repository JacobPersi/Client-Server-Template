using Microsoft.AspNetCore.SignalR;
using BlazorMinimalApp.Server.Data;

namespace BlazorMinimalApp.Server.Hubs
{
    public class TodoHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public TodoHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateTodoItem(int id, bool isComplete)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                todoItem.IsComplete = isComplete;
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("TodoItemUpdated", todoItem);
            }
        }

        public async Task AddTodoItem(string title)
        {
            var todoItem = new TodoItem { Title = title, IsComplete = false };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            await Clients.All.SendAsync("TodoItemAdded", todoItem);
        }

        public async Task DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("TodoItemDeleted", id);
            }
        }
    }
}