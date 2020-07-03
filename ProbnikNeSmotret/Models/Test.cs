using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProbnikNeSmotret.Models
{
    public class Test
    {
        public int Id { get; set; }

        [Display(Name = "Номер теста в главе")]
        public int NumInParagraph { get; set; }

        [Display(Name = "Название теста")]
        public string Name { get; set; }

        public List<Question> Questions { get; set; }

        [Display(Name = "Количество очков")]
        public int Points { get; set; }

        public int ParagraphId { get; set; }

        public Paragraph Paragraph { get; set; }
    }
}