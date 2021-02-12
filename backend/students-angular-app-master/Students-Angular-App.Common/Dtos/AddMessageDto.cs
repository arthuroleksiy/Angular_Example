using System;
using System.Collections.Generic;
using System.Text;

namespace Students_Angular_App.Common.Dtos
{
    public class AddMessageDto
    {

        public long UserId { get; set; }

        public long CourseId { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
