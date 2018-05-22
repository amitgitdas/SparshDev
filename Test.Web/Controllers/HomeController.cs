using SparshWeb.ModelBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SparshWeb.CustomAttribute;
using Test.Web.ModelBusiness;

namespace SparshWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //string path = "~\\XMLDB\\CommitteeMembersDB.xml";
            //ViewBag.Message = "Server Mappath " + System.Web.Hosting.HostingEnvironment.MapPath(path);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult YearlySchedule()
        {
            return View();
        }

        public ActionResult Members()
        {
            var list = CommitteeMemberBusiness.DisplayGroupMembers();
            return View(list);
        }

        public ActionResult Gallery()
        {
            var list = GalleryImageBusiness.ImageViewList();
            return View(list);
        }

        public ActionResult NoticeBoard()
        {
            string str = WebNoticeBusiness.DisplayNoticeBoard();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(Environment.NewLine, "<br/>");
                ViewBag.NoticeBoard = str;
            }
            else { ViewBag.NoticeBoard = string.Empty; }
            return View();
        }

        public ActionResult PujaCommittee()
        {
            return View();
        }
    }
}