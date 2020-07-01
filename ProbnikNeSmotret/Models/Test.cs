using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProbnikNeSmotret.Models
{
    public class Test
    {
        public int Id { get; set; }

        public int NumInParagraph { get; set; }

        public string Name { get; set; }

        public List<Question> Questions { get; set; }
        
        public int Points { get; set; }

        public int ParagraphId { get; set; }

        public Paragraph Paragraph { get; set; }
    }
}