using System;
using System.Collections.Generic;
using System.Text;

namespace Students_Angular_App.Common.Dtos
{
    public class UserRegisterDto
    {
        public string Login { get; set; }

        public string Password { get; set; }
        public long StudentId { get; set; }
    }
}