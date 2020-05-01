using Licenta.Dtos;
using Licenta.Helpers;
using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface IMessagesService
    {
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        IEnumerable<MessageForReturnDto> MapMessagesForUser(PagedList<Message> messages);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
        IEnumerable<MessageForReturnDto> MapMessagesForThread(IEnumerable<Message> messages);
        Message MapCreatedMessage(MessageForCreationDto messageForCreation);
        MessageForReturnDto MapMessageToReturn(Message message);
        void DeleteMessage(Message message);
        void MarkAsRead(Message message);
    }
}
