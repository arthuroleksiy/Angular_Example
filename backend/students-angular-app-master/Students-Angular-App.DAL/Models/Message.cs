using System;
using System.Collections.Generic;
using System.Text;

namespace Students_Angular_App.DAL.Models
{
    public class Message
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public long CourseId { get; set; }

        public Course Course { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
