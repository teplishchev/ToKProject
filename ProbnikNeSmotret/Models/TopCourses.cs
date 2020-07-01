using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class TopCourses
    {
        public List<KeyValuePair<Course, int>> Course { get; set; }
    }
}