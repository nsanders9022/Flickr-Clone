using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Flickr.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Flickr.Controllers
{
    [Authorize]
    public class PicturesController : Controller
    {
        private readonly FlickrDbContext _db;
        private readonly UserManager<FlickrUser> _userManager;

        public PicturesController (UserManager<FlickrUser> userManager, FlickrDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Pictures.Where(x => x.User.Id == currentUser.Id));
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
    }
}
