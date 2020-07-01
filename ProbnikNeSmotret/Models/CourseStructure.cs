using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class CourseStructure
    {
        public int Id { get; set; }

        public int  IdCourse { get; set; }

        public int NumParagraph { get; set; }

        public int HowManyOpen { get; set; }

        public List<ParagraphStructure> Paragraphs { get; set; }

        public virtual ICollection<PersonalArea> PersonalAreas { get; set; }

        public CourseStructure()
        {
            PersonalAreas = new List<PersonalArea>();
        }
    }

    public class ParagraphStructure
    {
        public int Id { get; set; }

        public int NumTest { get; set; }

        public int NumInCourse { get; set; }

        public int IdParagraph { get; set; }

        public int HowManyOpen { get; set; }

        public int CourseStructureId { get; set; }

        public CourseStructure CourseStructure { get; set; }

        public List<TestStructure> Tests { get; set; }
    }

    public class TestStructure
    {
        public int Id { get; set; }

        public int NumQuestion { get; set; }

        public int NumInParagraph { get; set; }

        public int IdTest { get; set; }

        public int HowManyOpen { get; set; }

        public List<QuestionStructure> QuestionStructures { get; set; }

        public int ParagraphStructureId { get; set; }

        public ParagraphStructure ParagraphStructure { get; set; }
    }

    public class QuestionStructure
    {
        public int Id { get; set; }

        public int IdQuestion { get; set; }

        public int TestStructureId { get; set; }

        public int NumInTest { get; set; }

        public bool Passed { get; set; }
    }

    //public class QuestionStruct
    //{
    //    public int Id { get; set; }

    //    public int IdQuestion { get; set; }

    //    public int TestStructureId { get; set; }

    //    public int NumInTest { get; set; }

    //    public bool Passed { get; set; }
    //}
}