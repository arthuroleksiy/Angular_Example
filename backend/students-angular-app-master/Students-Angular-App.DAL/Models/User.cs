using Students_Angular_App.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Students_Angular_App.DAL.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }        
        public string Password { get; set; }
        public string Role { get; set; }
        public Student Student { get; set; }

        public List<Message> Messages { get; set; }

        public User()
        {
            Messages = new List<Message>();
        }
    }
}
