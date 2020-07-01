using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ProbnikNeSmotret.Models
{
    [Table("Courses")]
    public class Course
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Пожалуйста введите название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название курса")]
        public string Name { get; set; }
        
        //[Required]
        [Range(0, 10000, ErrorMessage = "Недопустимая цена")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [StringLength(20000, MinimumLength = 20, ErrorMessage = "Полное описание должно быть от 20 до 20000 символов")]
        [Display(Name = "Полное описание")]
        public string FullDescription { get; set; }

        [StringLength(200, MinimumLength = 20, ErrorMessage = "Краткое описание должно быть от 20 до 200 символов")]
        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Paragraph> Paragraphs { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageUrl { get; set; }

        public virtual ICollection<PersonalArea> PersonalAreas { get; set; }

        public Course()
        {
            PersonalAreas = new List<PersonalArea>();
        }
    }
}