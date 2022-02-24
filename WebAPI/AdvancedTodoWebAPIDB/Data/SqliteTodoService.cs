using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedTodoWebAPIDB.Models;
using AdvancedTodoWebAPIDB.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdvancedTodoWebAPIDB.Data
{
    public class SqliteTodoService : ITodosService
    {
        private TodoContext ctx;

        public SqliteTodoService(TodoContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            EntityEntry<Todo> newlyAdded = await ctx.Todos.AddAsync(todo);
            await ctx.SaveChangesAsync();
            return newlyAdded.Entity;
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            Todo toDelete = await ctx.Todos.FirstOrDefaultAsync(t => t.TodoId == todoId);
            if (toDelete != null)
            {
                ctx.Todos.Remove(toDelete);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<Todo> UpdateAsync(Todo todo)
        {
            try
            {
                Todo toUpdate = await ctx.Todos.FirstAsync(t => t.TodoId == todo.TodoId);
                toUpdate.IsCompleted = todo.IsCompleted;
                ctx.Update(toUpdate);
                await ctx.SaveChangesAsync();
                return toUpdate;
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find todo with id{todo.TodoId}");
            }
        }

        public async Task<IList<Todo>> GetTodosAsync(int? userId, bool? isCompleted)
        {
            IQueryable<Todo> asQueryable = ctx.Todos.AsQueryable();
            if (userId != null)
            {
                asQueryable = asQueryable.Where(todo => todo.UserId == userId);
            }

            if (isCompleted != null)
            {
                asQueryable = asQueryable.Where(todo => todo.IsCompleted == isCompleted);
            }
            
            return await asQueryable.ToListAsync();
        }
    }
}