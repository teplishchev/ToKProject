using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class PersonalArea
    {
        public int Id { get; set; }

        public string AspNetUserId { get; set; }

        //public List<Course> Courses { get; set; }

        public string Person { get; set; }

        public virtual ICollection<CourseStructure> CourseStructures { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public PersonalArea()
        {
            Courses = new List<Course>();
            CourseStructures = new List<CourseStructure>();
        }
    }
}