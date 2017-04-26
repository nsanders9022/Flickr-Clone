using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flickr.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Flickr.Controllers 
{
    
    public class CommentsController : Controller
    {
        private FlickrDbContext _db = new FlickrDbContext();
        // GET: /<controller>/
        public IActionResult Create()
        {
            ViewBag.PictureId = new SelectList(_db.Pictures, "PictureId", "Caption");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return RedirectToAction("Index", "Pictures");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.PictureId = new SelectList(_db.Pictures, "PictureId", "Caption");
            var thisComment = _db.Comments.FirstOrDefault(comments => comments.CommentId == id);
            return View(thisComment);
        }

        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            _db.Entry(comment).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Pictures");
        }

        public IActionResult Delete(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(comments => comments.CommentId == id);
            return View(thisComment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(comments => comments.CommentId == id);
            _db.Comments.Remove(thisComment);
            _db.SaveChanges();
            return RedirectToAction("Index","Pictures");
        }
    }
}
