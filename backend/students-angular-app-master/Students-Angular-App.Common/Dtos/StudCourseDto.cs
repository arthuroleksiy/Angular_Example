using System;
using System.Collections.Generic;
using System.Text;

namespace Students_Angular_App.Common.Dtos
{
    public class StudCourseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
