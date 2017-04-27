using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Flickr.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Flickr.Controllers
{
    [Authorize]
    public class PicturesController : Controller
    {

        private readonly FlickrDbContext _db;
        private readonly UserManager<FlickrUser> _userManager;
        private FlickrDbContext _db1;

        public PicturesController (UserManager<FlickrUser> userManager, FlickrDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public PicturesController(FlickrDbContext _db1)
        {
            this._db1 = _db1;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Pictures.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Details(int id)
        {
            var thisPicture = _db.Pictures.Include(pictures => pictures.Comments).FirstOrDefault(pictures => pictures.PictureId == id);
            return View(thisPicture);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Picture picture)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            picture.User = currentUser;
            _db.Pictures.Add(picture);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisPicture = _db.Pictures.FirstOrDefault(pictures => pictures.PictureId == id);
            return View(thisPicture);
        }

        [HttpPost]
        public IActionResult Edit(Picture picture)
        {
            _db.Entry(picture).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisPicture = _db.Pictures.FirstOrDefault(pictures => pictures.PictureId == id);
            return View(thisPicture);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPicture = _db.Pictures.FirstOrDefault(pictures => pictures.PictureId == id);
            _db.Pictures.Remove(thisPicture);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
