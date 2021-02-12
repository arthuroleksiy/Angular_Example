using Microsoft.AspNetCore.SignalR;
using Student_Angular_App.BLL.Hubs;
using Students_Angular_App.Common.Dtos;
using Students_Angular_App.DAL.Models;
using Students_Angular_App.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services.Realizations
{
    public class MessageService : IMessageService
    {
        IRepository<Message> _messageRepository;
        IRepository<User> _userRepository;
        IHubContext<ChatHub> _chartHub;
        public MessageService(IRepository<Message> messageRepository, IRepository<User> userRepository, IHubContext<ChatHub> chartHub)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _chartHub = chartHub;
        }

        public async Task AddMessage(AddMessageDto addMessageDto)
        {
            await _messageRepository.CreateAsync(new Message
            {
                CourseId = addMessageDto.CourseId,
                Date = addMessageDto.Date,
                Text = addMessageDto.Text,
                UserId = addMessageDto.UserId
            });

           await _chartHub.Clients.All.SendAsync("getNewMessage", new MessageDto
            {
                Login = ( await _userRepository.GetByAsync(u => u.Id == addMessageDto.UserId)).Login,
                CourseId = addMessageDto.CourseId,
                Date = addMessageDto.Date,
                Text = addMessageDto.Text,
                UserId = addMessageDto.UserId
            });
        }

        public async Task<List<MessageDto>> GetMessages(long courseId)
        {
            var messages = await _messageRepository.GetAllByAsync(m => m.CourseId == courseId);
            messages.Reverse();
            var messageDtos = new List<MessageDto>();
            foreach(var message in messages) { 
                messageDtos.Add(new MessageDto
                {
                    Id = message.Id,
                    Date = message.Date,
                    CourseId = message.CourseId,
                    Login = (await _userRepository.GetByAsync( u => u.Id == message.UserId)).Login,
                    Text = message.Text,
                    UserId = message.UserId
                });
            }

            return messageDtos;
        }
    }
}
