using AutoMapper;
using Licenta.API.Data;
using Licenta.Dtos;
using Licenta.Helpers;
using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _messagesRepo;
        private readonly IMapper _mapper;
        private readonly IGenericsRepository _genericsRepo;

        public MessagesService(IMessagesRepository messagesRepo, IMapper mapper, IGenericsRepository genericsRepo)
        {
            _messagesRepo = messagesRepo;
            _mapper = mapper;
            _genericsRepo = genericsRepo;
        }

        public void DeleteMessage(Message message)
        {
            _genericsRepo.Delete(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _messagesRepo.GetMessage(id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            return await _messagesRepo.GetMessagesForUser(messageParams);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            return await _messagesRepo.GetMessageThread(userId, recipientId);
        }

        public Message MapCreatedMessage(MessageForCreationDto messageForCreation)
        {
            var message = _mapper.Map<Message>(messageForCreation);

            _genericsRepo.Add(message);

            return message;
        }

        public IEnumerable<MessageForReturnDto> MapMessagesForThread(IEnumerable<Message> messages)
        {
            return _mapper.Map<IEnumerable<MessageForReturnDto>>(messages);
        }

        public IEnumerable<MessageForReturnDto> MapMessagesForUser(PagedList<Message> messages)
        {
            return _mapper.Map<IEnumerable<MessageForReturnDto>>(messages);
        }

        public MessageForReturnDto MapMessageToReturn(Message message)
        {
            return _mapper.Map<MessageForReturnDto>(message);
        }

        public void MarkAsRead(Message message)
        {
            message.isRead = true;
            message.DateRead = DateTime.Now;
        }
    }
}
