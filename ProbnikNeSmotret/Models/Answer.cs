using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class AnswerViewModel
    { 
        public string Text { get; set; }

        public int AllPoints { get; set; }

        public Dictionary<int, string> answers { get; set; }
        public int TestPoints { get; set; }

        public Question Question { get; set; }

        public int NumInTest { get; set; }

        public int TestId { get; set; }

        public bool IsTheEnd { get; set; }
    }
}