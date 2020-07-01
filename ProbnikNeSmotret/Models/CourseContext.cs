using System.Data.Entity;


namespace ProbnikNeSmotret.Models
{
    public class CourseContext : DbContext
    {
        static CourseContext()
        {
            Database.SetInitializer<CourseContext>(null);
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<CourseStructure> CourseStructures { get; set; }

        //public DbSet<CoursePersonalArea> CoursePersonalAreas { get; set; }

        public DbSet<ParagraphStructure> ParagraphStructures { get; set; }

        public DbSet<TestStructure> TestStructures { get; set; }

        public DbSet<QuestionStructure> QuestionStructures { get; set; }

        public DbSet<PersonalArea> PersonalAreas { get; set; }

        public DbSet<Paragraph> Paragraphs { get; set; }

        public DbSet<Question> Questions { get; set; }

        //public DbSet<Answer> Answers { get; set; }
        //public DbSet<Item> Items { get; set; }
        //public DbSet<CartItem> ShoppingCarts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(c => c.PersonalAreas)
                .WithMany(s => s.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                .MapRightKey("PersonalAreaId")
                .ToTable("CoursePersonalArea"));

            modelBuilder.Entity<CourseStructure>().HasMany(c => c.PersonalAreas)
               .WithMany(s => s.CourseStructures)
               .Map(t => t.MapLeftKey("CourseStructureId")
               .MapRightKey("PersonalAreaId")
               .ToTable("CourseStructurePersonalArea"));
        }
    }
}