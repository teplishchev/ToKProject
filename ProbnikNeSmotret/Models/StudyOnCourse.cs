using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class StudyOnCourse
    {
        public int CourseId { get; set; }

        public int AspNetUserId { get; set; }

        public int PersonalAreaId { get; set; }

        public PersonalArea PersonalArea { get; set; }
    }
}