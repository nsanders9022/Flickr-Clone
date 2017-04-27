using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flickr.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Flickr.Controllers
{
    public class PicturesTagsController : Controller
    {
        private FlickrDbContext db = new FlickrDbContext();
        private readonly UserManager<FlickrUser> _userManager;

        //public IActionResult Index()
        //{
        //    return View(db.ExperiencesPeoples.ToList());
        //}

        public PicturesTagsController(UserManager<FlickrUser> userManager, FlickrDbContext db)
        {
            _userManager = userManager;
        
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            ViewBag.PictureId = new SelectList(db.Pictures.Where(x => x.User.Id == currentUser.Id), "PictureId", "Caption");
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
