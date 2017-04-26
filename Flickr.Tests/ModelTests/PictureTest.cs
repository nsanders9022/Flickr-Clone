using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flickr.Models;
using Xunit;

namespace Flickr.Tests
{
    public class PictureTest
    {
        [Fact]
        public void GetCaptionTest()
        {
            //Arrange
            var picture = new Picture();
            picture.Caption = "Space!";

            //Act
            var result = picture.Caption;

            //Assert
            Assert.Equal("Space!", result);
        }
    }
}
