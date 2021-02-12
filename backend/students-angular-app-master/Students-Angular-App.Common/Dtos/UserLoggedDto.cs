using System;
using System.Collections.Generic;
using System.Text;

namespace Students_Angular_App.Common.Dtos
{
    public class UserLoggedDto
    {
        public long UserId { get; set; }

        public long? StudentId { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
