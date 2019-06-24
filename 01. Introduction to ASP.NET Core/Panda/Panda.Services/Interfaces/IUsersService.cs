namespace Panda.Services.Interfaces
{
    using Panda.Domain.Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        string CreateUser(string username, string email, string password);

        PandaUser GetUserOrNull(string username, string password);

        IEnumerable<string> GetUsernames();
    }
}
