using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class Paragraph
    {
        public int Id { get; set; }

        public int NumInCourse { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<Test> ParagTests { get; set; }
    }
}