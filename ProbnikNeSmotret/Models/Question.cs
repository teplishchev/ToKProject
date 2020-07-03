using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProbnikNeSmotret.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Display(Name = "Номер вопроса в тесте")]
        public int NumInTest { get; set; }

        [Display(Name = "Вопрос")]
        public string Text { get; set; }

        [Display(Name = "Ответ")]
        public string Answer { get; set; }

        [Display(Name = "Количество очков")]
        public int Points { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        //public List<Answer> Answers { get; set; }

        public string ImgUrl { get; set; }
    }
}