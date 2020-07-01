using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProbnikNeSmotret.Models
{
    public class CourseViewModel
    {
        public Course TheCourse { get; set; }
        [Display(Name = "Отзывы")]
        public int ReviewNumber { get; set; }
    }

    public class ReviewViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}