using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class CourseTeacherViewModel
    {
        public Course Course { get; set; }

        public List<ParagraphTeacherViewModel> ParagraphsModwl { get; set; } 
    }

    public class ParagraphTeacherViewModel
    {
        public Paragraph Paragraph { get; set; }

        public List<TestTeacherViewModel> TestsModel { get; set; }
    }

    public class TestTeacherViewModel
    {
        public Test Test { get; set; }

        public List<Question> QuestionModels { get; set; }
    }
}