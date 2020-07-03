using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security;
using ProbnikNeSmotret.Models;
using Microsoft.AspNet.Identity;

namespace ProbnikNeSmotret.Controllers
{
    public class HomeController : Controller
    {
        CourseContext db = new CourseContext();

        //public ActionResult Index(int? categoryId)
        //{
        //    //var categoriesList = db.Categories.ToList();
        //    //categoriesList.Insert(0, new Category() { Id = 0, Name = "все" });
        //    //SelectList categories = new SelectList(categoriesList, "Id", "Name");
        //    //ViewBag.Genres = categories;

        //    //var books = db.Books.Include(b => b.Genre).OrderBy(b => b.Author);
        //    //if (!(categoryId == null || categoryId == 0))
        //    //{
        //    //    books = books.Where(b => b.CategoryId == categoryId).OrderBy(b => b.Author);
        //    //}

        //    //var list = books.ToList();
        //    //var bookViewList = new List<BookViewModel>();
        //    //foreach (var book in list)
        //    //{
        //    //    var item = new BookViewModel { TheBook = book };
        //    //    var reviews = db.Reviews.Count(x => x.BookId == book.Id);
        //    //    item.ReviewNumber = reviews;
        //    //    bookViewList.Add(item);
        //    //}
        //    //return View(bookViewList.ToList());
        //    return View();
        //}

        public ActionResult GetTopCourses()
        {
            TopCourses model = new TopCourses();
            List<KeyValuePair<Course, int>> sortMembers = TopCourse();
            model.Course = sortMembers;
            return View(model);
        }

        public List<KeyValuePair<Course, int>> TopCourse()
        {
            Dictionary<Course, int> members = new Dictionary<Course, int>();
            foreach (var course in db.Courses)
            {
                members.Add(course, 0);
            }
            List<string> userIds = new List<string>();
            foreach (var user in db.PersonalAreas)
            {
                userIds.Add(user.AspNetUserId);
            }
            foreach (var id in userIds)
            {
                var area = db.PersonalAreas.Where(p => p.AspNetUserId.CompareTo(id) == 0).First();
                foreach (var course in area.Courses)
                {
                    members[course]++;
                }
            }
            var sortMembers = members.OrderBy(m => m.Value).Reverse().ToList();
            return sortMembers;
        }

        [HttpGet]
        public ActionResult CheckTest(int testId, int number, bool isTheEnd, int testPoints, int allPoints, string text, Dictionary<int, string> answers)
        {
            AnswerViewModel answer = new AnswerViewModel() { AllPoints = allPoints, IsTheEnd = isTheEnd, TestId = testId, Text = text, TestPoints = testPoints, NumInTest =  number};
            if(answer.NumInTest != 0)
            {
                Question question1 = db.Questions.Where(q => q.NumInTest == number && q.TestId == answer.TestId).First();
                answer.Question = question1;
                answers.Add(number, text);
                answer.answers = answers;
                if (answer.Question.Answer.ToLower().CompareTo(answer.Text.ToLower()) == 0)
                {
                    int point = answer.AllPoints + answer.Question.Points;
                    answer.AllPoints = point;
                }
            }
            int numInTest = answer.NumInTest + 1;
            answer.NumInTest = numInTest;
            if(numInTest > db.Questions.Where(q => q.TestId == answer.TestId).Count())
            {
                string userId = User.Identity.GetUserId();
                PersonalArea area = db.PersonalAreas.Where(p => p.AspNetUserId.CompareTo(userId) == 0).First();
                Test test1 = db.Tests.Find(answer.TestId);
                int idPar = test1.ParagraphId;
                Paragraph parag = db.Paragraphs.Find(idPar);
                int courseId = parag.CourseId;
                ViewBag.CourseId = courseId;
                if (answer.TestPoints == answer.AllPoints)
                {
                    var structure = area.CourseStructures.Where(c => c.IdCourse == courseId).First();
                    var parStructures = db.ParagraphStructures.Where(p => p.CourseStructureId == structure.Id);
                    List<ParagraphStructure> sortParag = parStructures.OrderBy(p => p.NumInCourse).ToList();
                    foreach (var par in sortParag)
                    {
                        var testStructures = db.TestStructures.Where(t => t.ParagraphStructureId == par.Id);
                        List<TestStructure> testSort = testStructures.OrderBy(t => t.NumInParagraph).ToList();
                        foreach (var test in testSort)
                        {
                            if (test.IdTest == answer.TestId)
                            {
                                var queStructures = db.QuestionStructures.Where(q => q.TestStructureId == test.Id);
                                List<QuestionStructure> queSort = queStructures.OrderBy(q => q.NumInTest).ToList();
                                foreach (var que in queSort)
                                {
                                    que.Passed = true;
                                    db.Entry(que).State = System.Data.Entity.EntityState.Modified;
                                    
                                }
                            }
                        }
                    }
                    db.SaveChanges();
                }
                answer.IsTheEnd = true;
                return View(answer);
            }
            Question question = db.Questions.Where(q => q.NumInTest == numInTest && q.TestId == answer.TestId).First();
            answer.Question = question;
            answer.Text = "";
            return View(answer);
        }

        [HttpPost]
        public ActionResult CheckTest(AnswerViewModel answer)
        {
            return RedirectToAction("CheckTest", new { testId = answer.TestId, number = answer.NumInTest, isTheEnd = answer.IsTheEnd, testPoints = answer.TestPoints, allPoints = answer.AllPoints, text = answer.Text, answers = answer.answers});
        }

        public StudyViewModel GetNextParagraph(PersonalArea area, int courseId)
        {
            var structure = area.CourseStructures.Where(c => c.IdCourse == courseId).First();
            var parStructures = db.ParagraphStructures.Where(p => p.CourseStructureId == structure.Id);
            List<ParagraphStructure> sortParag = parStructures.OrderBy(p => p.NumInCourse).ToList();
            foreach (var par in sortParag)
            {
                var testStructures = db.TestStructures.Where(t => t.ParagraphStructureId == par.Id);
                List<TestStructure> testSort = testStructures.OrderBy(t => t.NumInParagraph).ToList();
                foreach (var test in testSort)
                {
                    bool isTestPassed = true;
                    var queStructures = db.QuestionStructures.Where(q => q.TestStructureId == test.Id);
                    List<QuestionStructure> queSort = queStructures.OrderBy(q => q.NumInTest).ToList();
                    foreach (var que in queSort)
                    {
                        if (que.Passed == false)
                        {
                            isTestPassed = false;
                        }
                    }
                    if (!isTestPassed)
                    {
                        List<Test> tests = db.Tests.Where(t => t.ParagraphId == par.IdParagraph).ToList();
                        StudyViewModel model = new StudyViewModel() { Paragraph = db.Paragraphs.Find(par.IdParagraph), Tests = tests };
                        return model;
                    }
                }
            }
            return new StudyViewModel() { Paragraph = db.Paragraphs.Find(sortParag.Last().IdParagraph), Tests = new List<Test>()};
        }

        public ActionResult StudyOnCourse(int courseId)
        {
            string userId = User.Identity.GetUserId();
            PersonalArea area = db.PersonalAreas.Where(p => p.AspNetUserId.CompareTo(userId) == 0).First();
            StudyViewModel model = GetNextParagraph(area, courseId);
            return View(model);
        }

        public ActionResult DeleteCourseFromPersonalArea(int courseId)
        {
            string userId = User.Identity.GetUserId();
            PersonalArea area = db.PersonalAreas.Where(p => p.AspNetUserId.CompareTo(userId) == 0).First();
            // area.Courses.Remove(db.Courses.Find(courseId));
            // 
            CourseStructure corStr = area.CourseStructures.Where(c => c.IdCourse == courseId).First();
            area.CourseStructures.Remove(area.CourseStructures.Where(c => c.IdCourse == courseId).First());
            db.CourseStructures.Remove(corStr);
            area.Courses.Remove(db.Courses.Find(courseId));
            db.Entry(area).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            CoursesInAreaModel coursesModel = MakeCoursesModel(area);
            ViewBag.coursesModel = coursesModel;
            List<KeyValuePair<int, int>> redGreen = new List<KeyValuePair<int, int>>();
            foreach (var course in area.Courses)
            {
                StudyViewModel studyModel = GetNextParagraph(area, course.Id);
                redGreen.Add(new KeyValuePair<int, int>(course.Id, studyModel.Paragraph.NumInCourse));
            }
            ViewBag.RedGreen = redGreen;
            return View(area);
        }

        public CoursesInAreaModel MakeCoursesModel(PersonalArea area)
        {
            CoursesInAreaModel coursesModel = new CoursesInAreaModel();
            Dictionary<int, List<Triplet>> dictionary = new Dictionary<int, List<Triplet>>();
            foreach (var course in area.Courses)
            {
                var paragraphs = db.Paragraphs.Where(p => p.CourseId == course.Id).ToList();
                var sort = paragraphs.OrderBy(p => p.NumInCourse).ToList();
                List<Triplet> pars = new List<Triplet>();
                foreach (var paragraph in sort)
                {
                    pars.Add(new Triplet() { Name = paragraph.Name, UrlImg = paragraph.ImgUrl, ParagraphId = paragraph.Id });
                }
                dictionary.Add(course.Id, pars);
            }
            coursesModel.courses = dictionary;
            return coursesModel;
        }

        public ActionResult ShowArea()
        {
            //string userId1 = User.Identity.GetUserId();
            //PersonalArea area1 = db.PersonalAreas.Where(a => a.AspNetUserId.CompareTo(userId1) == 0).First();
            //db.PersonalAreas.Remove(area1);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.teacher = false;
                if (User.IsInRole("teacher"))
                {
                    PersonalArea areaTeacher = new PersonalArea() {AspNetUserId = User.Identity.GetUserId(), Person = User.Identity.Name };
                    ViewBag.teacher = true;
                    return View(areaTeacher);
                }
                string userId = User.Identity.GetUserId();
                PersonalArea area = db.PersonalAreas.Where(a => a.AspNetUserId.CompareTo(userId) == 0).First();
                CoursesInAreaModel coursesModel = MakeCoursesModel(area);
                ViewBag.coursesModel = coursesModel;
                List<KeyValuePair<int, int>> redGreen = new List<KeyValuePair<int, int>>();
                foreach(var course in area.Courses)
                {
                    StudyViewModel studyModel = GetNextParagraph(area, course.Id);
                    redGreen.Add(new KeyValuePair<int, int>(course.Id, studyModel.Paragraph.NumInCourse));
                }
                ViewBag.RedGreen = redGreen;
                return View(area);
            }
            return RedirectToAction("Index");

        }


        public ActionResult AddCourseToArea(int courseId)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.teacher = false;
                if (User.IsInRole("teacher"))
                {
                    PersonalArea areaTeacher = new PersonalArea() { AspNetUserId = User.Identity.GetUserId(), Person = User.Identity.Name };
                    ViewBag.teacher = true;
                    return View(areaTeacher);
                }
                string userId = User.Identity.GetUserId();
                PersonalArea area = db.PersonalAreas.Where(a => a.AspNetUserId.CompareTo(userId) == 0).First();
                bool alreadyStudy = false;
                foreach(var course in area.Courses)
                {
                    if(course.Id == courseId)
                    {
                        alreadyStudy = true;
                    }
                }
                if (alreadyStudy)
                {
                    CoursesInAreaModel coursesModel1 = MakeCoursesModel(area);
                    ViewBag.coursesModel = coursesModel1;
                    List<KeyValuePair<int, int>> redGreen1 = new List<KeyValuePair<int, int>>();
                    foreach (var course in area.Courses)
                    {
                        StudyViewModel studyModel = GetNextParagraph(area, course.Id);
                        redGreen1.Add(new KeyValuePair<int, int>(course.Id, studyModel.Paragraph.NumInCourse));
                    }
                    ViewBag.RedGreen = redGreen1;
                    return View(area);
                }
                CourseStructure courseStr = CreateCourseStructure(courseId);
                area.CourseStructures.Add(courseStr);
                area.Courses.Add(db.Courses.Find(courseId));
                db.SaveChanges();
                CoursesInAreaModel coursesModel = MakeCoursesModel(area);
                ViewBag.coursesModel = coursesModel;
                List<KeyValuePair<int, int>> redGreen = new List<KeyValuePair<int, int>>();
                foreach (var course in area.Courses)
                {
                    StudyViewModel studyModel = GetNextParagraph(area, course.Id);
                    redGreen.Add(new KeyValuePair<int, int>(course.Id, studyModel.Paragraph.NumInCourse));
                }
                ViewBag.RedGreen = redGreen;
                return View(area);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult WatchParagraph(int paragraphId)
        {
            var paragraph = db.Paragraphs.Find(paragraphId);
            return View(paragraph);
        }

        public CourseStructure CreateCourseStructure(int courseId)
        {
            Course course = db.Courses.Find(courseId);
            CourseStructure courseStructure = new CourseStructure();
            courseStructure.IdCourse = course.Id;
            List<ParagraphStructure> listParag = new List<ParagraphStructure>();
            var paragraphs = db.Paragraphs.Where(p => p.CourseId == courseId).ToList();
            foreach (var par in paragraphs)
            {
                List<TestStructure> tests = new List<TestStructure>();
                var t = db.Tests.Where(te => te.ParagraphId == par.Id).ToList();
                foreach (var test in t)
                {
                    var questions = db.Questions.Where(q => q.TestId == test.Id);
                    TestStructure testStructure = new TestStructure() { NumQuestion = questions.Count(), IdTest = test.Id, NumInParagraph = test.NumInParagraph };
                    List<QuestionStructure> queStructure = new List<QuestionStructure>();
                    foreach (var que in questions)
                    {
                        QuestionStructure q = new QuestionStructure() { Passed = false, NumInTest = que.NumInTest, IdQuestion = que.Id };
                        queStructure.Add(q);
                    }
                    testStructure.QuestionStructures = queStructure;
                    db.TestStructures.Add(testStructure);
                    tests.Add(testStructure);
                }
                ParagraphStructure paragraphStructure = new ParagraphStructure() { NumTest = tests.Count, Tests = tests, IdParagraph = par.Id, NumInCourse = par.NumInCourse };
                db.ParagraphStructures.Add(paragraphStructure);
                listParag.Add(paragraphStructure);
            }
            courseStructure.NumParagraph = listParag.Count();
            courseStructure.Paragraphs = listParag;
            db.CourseStructures.Add(courseStructure);
            db.SaveChanges();
            return courseStructure;
        }

        public ActionResult Index(int? categoryId)
        {
            var categoriesList = db.Categories.ToList();
            categoriesList.Insert(0, new Category() { Id = 0, Name = "все" });
            SelectList categories = new SelectList(categoriesList, "Id", "Name");
            ViewBag.Genres = categories;
            string role = "none";
            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole("teacher")) role = "teacher";
                if (User.IsInRole("user")) role = "user";
                if (User.IsInRole("admin")) role = "admin";
            }
            ViewBag.Role = role;
            var courses = db.Courses.Include(b => b.Category).OrderBy(b => b.Name);
            if (!(categoryId == null || categoryId == 0))
            {
                courses = courses.Where(b => b.CategoryId == categoryId).OrderBy(b => b.Name);
            }

            var list = courses.ToList();
            var courseViewList = new List<CourseViewModel>();
            foreach (var course in list)
            {
                var item = new CourseViewModel { TheCourse = course };
                var reviews = db.Reviews.Count(x => x.CourseId == course.Id);
                item.ReviewNumber = reviews;
                courseViewList.Add(item);
            }
            return View(courseViewList.ToList());
            //return View();
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return RedirectToAction("Index");
            var result = db.Courses.Include(c => c.Category).Where(c => c.Category.Name.Contains(searchString)).ToList();
            result.AddRange(db.Courses.Include(c => c.Category).Where(c => c.Name.Contains(searchString)).ToList());
            result.AddRange(db.Courses.Include(c => c.Category).Where(c => c.ShortDescription.Contains(searchString)).ToList());
            var res = result.Distinct();

            //if (result.Count > 0)
            //{
                ViewBag.Message = new[] { "Результаты поиска" };
            //}
            //else
            //{
            //    ViewBag.Message = new[] { "Мы ничего не нашли по вашему запросу! Что делать?",
            //        "Посмотрите на книги, которые пользуются спросом у наших покупателей"};
            //    var orders = db.Orders.Take(20).ToList();
            //    var booksDictionary = new Dictionary<int, int>();
            //    foreach (var order in orders)
            //    {
            //        var books = db.Items.Where(i => i.OrderId == order.Id).ToList();
            //        foreach (var book in books)
            //        {
            //            if (booksDictionary.ContainsKey(book.BookId))
            //            {
            //                booksDictionary[book.BookId] += book.Quantity;
            //            }
            //            else booksDictionary.Add(book.BookId, book.Quantity);
            //        }
            //    }
            //    var idList = (from t in booksDictionary orderby t.Value descending select t).Take(6);

            //    foreach (var item in idList)
            //    {
            //        var book = db.Books.Find(item.Key);
            //        if (book == null) continue;
            //        var zanr = db.Categories.Find(book.CategoryId);
            //        book.Genre = zanr;
            //        result.Add(book);
            //    }
            //}
            return View(res);
        }

        public ActionResult About()
        {
            string role = "none";
            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole("teacher")) role = "teacher";
                if (User.IsInRole("user")) role = "user";
                if (User.IsInRole("admin")) role = "admin";
            }
            ViewBag.Role = role;
            List<KeyValuePair<Course, int>> sort = TopCourse();
            List<Course> courses = new List<Course>();
            foreach(var category in db.Categories)
            {
                foreach(var course in sort)
                {
                    if(course.Key.CategoryId == category.Id)
                    {
                        courses.Add(course.Key);
                        break;
                    }
                }
            }
            return View(courses);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult ViewBookReviews(int bookId)
        //{
        //    Book book = db.Books.Find(bookId);
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    book.Genre = db.Categories.Find(book.CategoryId);
        //    ViewBag.Book = book;
        //    var reviews = db.Reviews.Where(x => x.BookId == bookId).ToList();
        //    var list = new List<ReviewViewModel>();
        //    foreach (var item in reviews)
        //    {
        //        var note = new ReviewViewModel { Text = item.Text };
        //        var client = db.Clients.Find(item.ClientId);
        //        if (client != null)
        //        {
        //            note.Name = client.Name;
        //            note.LastName = client.LastName;
        //        }
        //        list.Add(note);
        //    }

        //    return View(list);
        //}
    }
}