using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparshWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SparshWeb.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTest
    {
        [TestMethod()]
        public void AboutTest()
        {
            HomeController home = new HomeController();
            ViewResult result = home.About() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void IndexTest()
        {
            HomeController home = new HomeController();
            ViewResult result = home.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ContactTest()
        {
            HomeController home = new HomeController();
            ViewResult result = home.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void YearlyScheduleTest()
        {
            HomeController home = new HomeController();
            ViewResult result = home.YearlySchedule() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void MembersTest()
        {
            HomeController home = new HomeController();
            ViewResult result = home.Members() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GalleryTest()
        {
            HomeController home = new HomeController();
            ViewResult result = home.Gallery() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}