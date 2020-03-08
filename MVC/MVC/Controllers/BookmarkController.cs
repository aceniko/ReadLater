using MVC.Models;
using ReadLater.Entities;
using ReadLater.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly ICategoryService _categoryService;

        public BookmarkController(IBookmarkService bookmarkService, ICategoryService categoryService) {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
        }

        // GET: Bookmark
        public ActionResult Index(string category)
        {
            ViewBag.category = category;
            return View();
        }

        public ActionResult BookmarkList(string category) {
            var model = _bookmarkService.GetBookmarks(category);
            return PartialView("~/Views/Bookmark/Partial/_BookmarkList.cshtml", model);
        }

        public ActionResult Create() {
            BookmarkCreateViewModel model = new BookmarkCreateViewModel {
                bookmark = new Bookmark(),
                categories = _categoryService.GetCategories()
            };

            return PartialView("~/Views/Bookmark/Partial/_Create.cshtml", model);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookmarkCreateViewModel model) {

            try
            {
                if (!Request.IsAjaxRequest())
                {
                    return new HttpNotFoundResult();
                }

                _bookmarkService.CreateBookmark(model.bookmark);

                return Json(new { error = string.Empty, success = true });
            }
            catch (Exception e) {
                return Json(new { error = e.Message, success = false });
            }
        }

        public ActionResult Edit(int id) {
            var bookmark = _bookmarkService.GetBookmarks(null).FirstOrDefault(x => x.ID == id);

            BookmarkCreateViewModel model = new BookmarkCreateViewModel
            {
                bookmark = bookmark,
                categories = _categoryService.GetCategories()
            };

            

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Bookmark model) {
            
            _bookmarkService.UpdateBookmark(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {
            _bookmarkService.DeleteBookmark(id);
            return RedirectToAction("Index");
        }
    }
}