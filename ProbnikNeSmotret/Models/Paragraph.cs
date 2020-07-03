using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbnikNeSmotret.Models
{
    public class Paragraph
    {
        public int Id { get; set; }

        [Display(Name = "Номер главы в курсе")]
        public int NumInCourse { get; set; }

        [Display(Name = "Название главы")]
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        [Display(Name = "Описание главы")]
        public string Description { get; set; }

        [Display(Name = "Текст главы")]
        public string Text { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<Test> ParagTests { get; set; }
    }
}