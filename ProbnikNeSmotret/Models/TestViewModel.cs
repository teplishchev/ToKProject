using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class TestViewModel
    {
        public Test Test { get; set; }

        public int TrueAnswersCount { get; set; }

        public List<Question> Questions { get; set; }

        public Dictionary<Question, string> Answers { get; set; }
    }
}