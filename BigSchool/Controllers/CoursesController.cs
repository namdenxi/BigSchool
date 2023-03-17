using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    [Authorize]

    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _Context;
        // GET: Courses
        public CoursesController()
        {
            _Context = new ApplicationDbContext();
        }
        public ActionResult Create()
        {
            var listCategories = new CourseViewlModel
            {
                Categories = _Context.Categories.ToList()
            };
            return View(listCategories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewlModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _Context.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerID = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryID = viewModel.Category,
                Place = viewModel.Place
            };
            _Context.Courses.Add(course);
            _Context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}