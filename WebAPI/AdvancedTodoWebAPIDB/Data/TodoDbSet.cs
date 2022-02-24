using System.Threading;
using System.Threading.Tasks;
using AdvancedTodoWebAPIDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdvancedTodoWebAPIDB.Data
{
    public class TodoDbSet : DbSet<Todo>
    {
        public override ValueTask<EntityEntry<Todo>> AddAsync(Todo entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return base.AddAsync(entity, cancellationToken);
        }
    }
}