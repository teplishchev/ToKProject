using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using ProbnikNeSmotret.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data.Entity;

namespace ProbnikNeSmotret.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        // GET: Teacher
        CourseContext db = new CourseContext();

        private const int Height = 400, Width = 250;

        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            var list = db.Courses;
            return View(list);
        }

        [HttpGet]
        public ActionResult TestListToCorrect(int paragraphId)
        {
            var tests = db.Tests.Where(t => t.ParagraphId == paragraphId);
            return View(tests);
        }

        [HttpGet]
        public ActionResult CorrectTest(int testId)
        {
            var test = db.Tests.Find(testId);
            return View(test);
        }

        [HttpGet]
        public ActionResult QuestionListToCorrect(int testId)
        {
            var questions = db.Questions.Where(q => q.TestId == testId);
            return View(questions);
        }

        [HttpGet]
        public ActionResult CorrectQuestion(int questionId)
        {
            var question = db.Questions.Find(questionId);
            return View(question);
        }

        [HttpPost]
        public ActionResult CorrectQuestion(Question question, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                if (CheckByGraphicsFormat(fileName))
                {
                    SaveImage(upload, fileName);
                    question.ImgUrl = fileName;
                }
            }
            if (!ModelState.IsValid) return View(question);
            db.Entry(question).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CorrectTest(Test test)
        {
            if (!ModelState.IsValid) return View(test);
            db.Entry(test).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CorrectParagraph(int paragraphId)
        {
            var paragraph = db.Paragraphs.Find(paragraphId);
            return View(paragraph);
        }

        [HttpPost]
        public ActionResult CorrectParagraph(Paragraph par, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                if (CheckByGraphicsFormat(fileName))
                {
                    SaveImage(upload, fileName);
                    par.ImgUrl = fileName;
                }
            }
            if (!ModelState.IsValid) return View(par);
            db.Entry(par).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ParagraphListToCorrect(int courseId)
        {
            ViewBag.courseId = courseId;
            var paragraphs = db.Paragraphs.Where(p => p.CourseId == courseId);
            return View(paragraphs);
        }

        [HttpGet]
        public ActionResult CorrectCourse(int courseId)
        {
            var categoriesList = db.Categories.Where(c => c.Id > 0).ToList();
            //List<Category> list = new List<Category>() { categoriesList };
            SelectList categories = new SelectList(categoriesList, "Id", "Name");
            ViewBag.categories = categories;
            var course = db.Courses.Find(courseId);
            return View(course);
        }

        [HttpPost]
        public ActionResult CorrectCourse(Course course, HttpPostedFileBase upload)
        {
            var categoriesList = db.Categories.Where(c => c.Id > 0).ToList();
            //List<Category> list = new List<Category>() { categoriesList };
            SelectList categories = new SelectList(categoriesList, "Id", "Name");
            ViewBag.categories = categories;
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                if (CheckByGraphicsFormat(fileName))
                {
                    SaveImage(upload, fileName);
                    course.ImageUrl = fileName;
                }
            }
            if (!ModelState.IsValid) return View(course);
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateParagraph(int courseId)
        {
            var course = db.Courses.Find(courseId);
            var paragraphs = db.Paragraphs.Where(p => p.CourseId == courseId).ToList();
            int maxParagraph = 0;
            foreach(var par in paragraphs)
            {
                int num = par.NumInCourse;
                if (maxParagraph < num) maxParagraph = num;
            }
            List<Course> courseList = new List<Course>() { course };
            SelectList courses = new SelectList(courseList, "Id", "Name");
            ViewBag.list = courses;
            ViewBag.numberOfParagraph = maxParagraph + 1;
            return View();
        }

        public ActionResult DeleteCourse(int courseId)
        {
            db.Courses.Remove(db.Courses.Find(courseId));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ParagraphListOnDelete(int courseId)
        {
            var parags = db.Paragraphs.Where(p => p.CourseId == courseId);
            return View(parags);
        }

        public ActionResult TestListOnDelete(int paragraphId)
        {
            var tests = db.Tests.Where(t => t.ParagraphId == paragraphId);
            return View(tests);
        }

        public ActionResult ParagraphListOnDeleteTest(int courseId)
        {
            var parags = db.Paragraphs.Where(p => p.CourseId == courseId);
            return View(parags);
        }

        public ActionResult DeleteParagraph(int paragraphId)
        {
            db.Paragraphs.Remove(db.Paragraphs.Find(paragraphId));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTest(int testId)
        {
            db.Tests.Remove(db.Tests.Find(testId));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteQuestion(int questionId)
        {
            db.Questions.Remove(db.Questions.Find(questionId));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParagraph(Paragraph paragraph, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                if (CheckByGraphicsFormat(fileName))
                {
                    SaveImage(upload, fileName);
                    paragraph.ImgUrl = fileName;
                }
                else paragraph.ImgUrl = "virus1.jpg";
            }
            if (!ModelState.IsValid) return View(paragraph);
            db.Paragraphs.Add(paragraph);
            //Course course = db.Courses.Where(u => u.Id == paragraph.CourseId).First();
            //course.Paragraphs.Add(paragraph);
            //db.Entry(course).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateTest(int courseId)
        {
            var paragraphsList = db.Paragraphs.Where(p => p.CourseId == courseId).ToList();
            SelectList paragraphs = new SelectList(paragraphsList, "Id", "Name");
            ViewBag.list = paragraphs;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTest(Test test)
        {
            if (!ModelState.IsValid) return View(test);
            db.Tests.Add(test);
            //Paragraph paragraph = db.Paragraphs.Where(p => p.Id == test.ParagraphId).First();
            //paragraph.ParagTests.Add(test);
            //db.Entry(paragraph).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ParagraphsList(int courseId)
        {
            var parags = db.Paragraphs.Where(p => p.CourseId == courseId);
            return View(parags);
        }

        [HttpGet]
        public ActionResult TestsList(int paragraphId)
        {
            var tests = db.Tests.Where(t => t.ParagraphId == paragraphId);
            return View(tests);
        }

        [HttpGet]
        public ActionResult CreateQuestion(int testId)
        {
            var test = db.Tests.Find(testId);
            List<Test> tests = new List<Test>();
            tests.Add(test);
            SelectList paragraphs = new SelectList(tests, "Id", "Name");
            ViewBag.testId = paragraphs;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(Question question, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                if (CheckByGraphicsFormat(fileName))
                {
                    SaveImage(upload, fileName);
                    question.ImgUrl = fileName;
                }
                else question.ImgUrl = "virus1.jpg";
            }
            if (!ModelState.IsValid) return View(question);
            db.Questions.Add(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult QuestionsList(int testId)
        {
            var list = db.Questions.Where(q => q.TestId == testId).ToList();
            return View(list);
        }

        //[HttpGet]
        //public ActionResult CreateAnswer(int questionId)
        //{
        //    var que = db.Questions.Find(questionId);
        //    List<Question> ques = new List<Question>();
        //    ques.Add(que);
        //    SelectList paragraphs = new SelectList(ques, "Id", "Text");
        //    ViewBag.questionId = paragraphs;
        //    return View();
        //}

        //[HttpPost]
        //public void CreateAnswer(Answer answer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Answers.Add(answer);
        //        db.SaveChanges();
        //    }
        //}

        [HttpGet]
        public ActionResult CreateCourse()
        {
            var categoriesList = db.Categories.Where(c => c.Id > 0).ToList();
            //List<Category> list = new List<Category>() { categoriesList };
            SelectList categories = new SelectList(categoriesList, "Id", "Name");
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(Course course, HttpPostedFileBase upload)
        {
            var categoriesList = db.Categories.Where(c => c.Id > 0).ToList();
            //List<Category> list = new List<Category>() { categoriesList };
            SelectList categories = new SelectList(categoriesList, "Id", "Name");
            ViewBag.categories = categories;
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                if (CheckByGraphicsFormat(fileName))
                {
                    SaveImage(upload, fileName);
                    course.ImageUrl = fileName;
                }
                else course.ImageUrl = "virus1.jpg";
            }
            if (!ModelState.IsValid) return View(course);
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SaveImage(HttpPostedFileBase upload, string fileName)
        {
            var image = new Bitmap(upload.InputStream);
            var smallImg = ResizeImage(image, Width, Height);
            smallImg.Save(Server.MapPath("~/Images/" + fileName));
        }

        private bool CheckByGraphicsFormat(string fileName)
        {
            var ext = fileName.Substring(fileName.Length - 3);
            return string.Compare(ext, "png", StringComparison.Ordinal) == 0 ||
                   string.Compare(ext, "jpg", StringComparison.Ordinal) == 0;
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        public ActionResult CreateCategoty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            bool exist = false;
            foreach (var cat in db.Categories)
            {
                if (cat.Name == category.Name)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                return RedirectToAction("Index");
                //return View();

            }
            else
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
                //return View();
            }
        }

        [HttpGet]
        public ActionResult CourseDetails(int courseId)
        {
            CourseTeacherViewModel model = new CourseTeacherViewModel();
            Course course = db.Courses.Find(courseId);
            model.Course = course;
            List<ParagraphTeacherViewModel> paragraphsModel = new List<ParagraphTeacherViewModel>();
            var listPar = db.Paragraphs.Where(p => p.CourseId == courseId).ToList();
            foreach(var paragraph in listPar)
            {
                List<TestTeacherViewModel> testsModel = new List<TestTeacherViewModel>();
                var listTest = db.Tests.Where(t => t.ParagraphId == paragraph.Id).ToList();
                foreach (var test in listTest)
                {
                    List<Question> queModel = new List<Question>();
                    var listQue = db.Questions.Where(q => q.TestId == test.Id).ToList();
                    TestTeacherViewModel testM = new TestTeacherViewModel() { Test = test, QuestionModels = queModel };
                    testsModel.Add(testM);
                }
                ParagraphTeacherViewModel paragraphM = new ParagraphTeacherViewModel() { Paragraph = paragraph, TestsModel = testsModel };
                paragraphsModel.Add(paragraphM);
            }
            model.ParagraphsModwl = paragraphsModel;
            List<CourseTeacherViewModel> list = new List<CourseTeacherViewModel>();
            list.Add(model);
            return View(model);
        }
    }
}