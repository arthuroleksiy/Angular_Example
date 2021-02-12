using Students_Angular_App.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student_Angular_App.BLL.Services
{
    public interface IMessageService
    {
        Task AddMessage(AddMessageDto addMessageDto);

        Task<List<MessageDto>> GetMessages(long courseId);

    }
}
