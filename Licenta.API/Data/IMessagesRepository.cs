using Licenta.Helpers;
using Licenta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.API.Data
{
    public interface IMessagesRepository
    {
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messsageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    }
}
