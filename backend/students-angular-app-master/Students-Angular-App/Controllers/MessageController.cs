using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Angular_App.BLL.Services;
using Students_Angular_App.Common.Dtos;

namespace Students_Angular_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<List<MessageDto>> GetMessages([FromQuery] long courseId)
        {
            try
            {
                return await _messageService.GetMessages(courseId);
            }
            catch(Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        [HttpPost]
        public async Task AddMessage([FromBody]AddMessageDto addMessageDto)
        {
            try
            {
                 await _messageService.AddMessage(addMessageDto);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}
