using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flickr.Controllers;
using Flickr.Models;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace Flickr.Tests.ControllerTests
{
    public class PicturesControllerTest
    {
        private readonly FlickrDbContext _db;
        private readonly UserManager<FlickrUser> _userManager;

        [Fact]
        public void Get_ViewResult_Index_Test()
        {
            //Arrange
            PicturesController controller = new PicturesController(_userManager, _db);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
