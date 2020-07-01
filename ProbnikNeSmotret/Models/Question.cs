using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int NumInTest { get; set; }

        public string Text { get; set; }

        public string Answer { get; set; }

        public int Points { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        //public List<Answer> Answers { get; set; }

        public string ImgUrl { get; set; }
    }
}