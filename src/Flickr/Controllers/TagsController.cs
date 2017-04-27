using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flickr.Models;


namespace Flickr.Controllers
{
    public class TagsController : Controller
    {
        private FlickrDbContext _db = new FlickrDbContext();
        public IActionResult Index()
        {
            ViewBag.Picture = _db.Tags
               .Include(tag => tag.PicturesTags)
               .ThenInclude(picturesTags => picturesTags.Picture);
               //.Where(tag => tag.TagId == id).ToList();
            return View(_db.Tags.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.PictureId = new SelectList(_db.Pictures, "PictureId", "Caption");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.TagId = new SelectList(_db.Tags, "PictureId", "Caption");
            var thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
            return View(thisTag);
        }

        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            _db.Entry(tag).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
            return View(thisTag);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
            _db.Tags.Remove(thisTag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
