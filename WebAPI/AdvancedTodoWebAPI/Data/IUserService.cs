using System.Threading.Tasks;
using AdvancedTodoWebAPI.Models;

namespace AdvancedTodoWebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string userName, string passWord);
    }
}