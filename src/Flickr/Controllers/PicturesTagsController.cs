using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flickr.Models;



namespace Flickr.Controllers
{
    public class PicturesTagsController : Controller
    {
        private FlickrDbContext db = new FlickrDbContext();

        //public IActionResult Index()
        //{
        //    return View(db.ExperiencesPeoples.ToList());
        //}

        public IActionResult Create()
        {
            ViewBag.PictureId = new SelectList(db.Pictures, "PictureId", "Caption");
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Word");
            return View();
        }

        [HttpPost]
        public IActionResult Create(PictureTag pictureTag)
        {
            db.PicturesTags.Add(pictureTag);
            db.SaveChanges();
            return RedirectToAction("Index", "Pictures");
        }
    }
}
