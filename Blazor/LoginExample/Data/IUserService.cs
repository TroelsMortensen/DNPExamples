using LoginExample.Models;

namespace LoginExample.Data {
public interface IUserService {
    User ValidateUser(string userName, string password);
}
}