using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SparshWeb.Models;
using PagedList;
using System.Net;
using SparshWeb.XMLHelper;
using CaptchaMvc.HtmlHelpers;
using System.Security.Principal;
using System.Threading;
using SparshWeb.CustomAttribute;
using System.Configuration;

namespace SparshWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [CustomAuthentication]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthentication]
        public ActionResult WebNotice(int? page)
        {
            IList<WebNoticeViewModel> wnList = XMLWebNoticeReaderWriter.ReadWebNoticeData();
            int pageSize = 25;
            int pageNumber = (page ?? 1);

            return View(wnList.ToPagedList(pageNumber, pageSize));
            //return View(wnList.ToList());
        }

        [HttpGet]
        [CustomAuthentication]
        public ActionResult CreateWebNotice()
        {
            WebNoticeViewModel wn = new WebNoticeViewModel();
            return View(wn);
        }

        [CustomAuthentication]
        public ActionResult SMSNotice()
        {
            return View();
        }

        private string sendSMS()
        {
            //String message = HttpUtility.UrlEncode("This is your message. This TEst SMS from C# Code.");
            //try
            //{
            //    using (var wb = new WebClient())
            //    {
            //        byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new System.Collections.Specialized.NameValueCollection()
            //    {
            //    {"apikey" , "tunugzWhqgk-Z3iSYBm7m3hDCOc10aRRNG8vk3CzBO"},
            //    {"numbers" , "918697837899"},
            //    {"message" , message},
            //    {"sender" , "TXTLCL"}
            //    });
            //        string result = System.Text.Encoding.UTF8.GetString(response);
            //        return result;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    throw new Exception(ex.Message, ex);
            //}
            return "sss";
        }

        public ActionResult SetLogin()
        {
            AdminUser usr = new AdminUser();
            return View(usr);
        }

        [HttpPost]
        public ActionResult SetLogin(AdminUser usr)
        {
            if (!string.IsNullOrEmpty(usr.UserName) && !string.IsNullOrEmpty(usr.UserPassword))
                if (this.IsCaptchaValid("Captcha is not valid"))
                {
                    bool result = false;
                    usr.UserRole = "Admin";
                    string userName = ConfigurationManager.AppSettings["AdminUser"].Trim();
                    string userpwd = ConfigurationManager.AppSettings["AdminPwd"].Trim();
                    if(usr.UserName.ToUpper().Trim() == userName.Trim().ToUpper()
                        && usr.UserPassword.ToUpper().Trim() == userpwd.Trim().ToUpper())
                    {
                        result = true;
                    }
                     
                    if (result)
                    {
                        var identity = new System.Security.Principal.GenericIdentity(usr.UserName);
                        var principal = new GenericPrincipal(identity, new string[0]);
                        HttpContext.User = principal;
                        Thread.CurrentPrincipal = principal;

                        string loggedUser = HttpContext.User.Identity.Name;
                        System.Web.Security.FormsAuthentication.SetAuthCookie(loggedUser, false);
                        var name = System.Web.Security.FormsAuthentication.FormsCookieName;
                        var cookie = Response.Cookies[name];
                        if (cookie != null)
                        {
                            var ticket = System.Web.Security.FormsAuthentication.Decrypt(cookie.Value);
                            if (ticket != null && !ticket.Expired)
                            {
                                string[] roles = (ticket.UserData as string ?? "").Split(',');
                                System.Web.HttpContext.Current.User = new GenericPrincipal(new System.Web.Security.FormsIdentity(ticket), roles);
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["failedMessage"] = "Successfully Login Profile has been set for TCA Mobile Application";
                        return RedirectToAction("LoginFailed");
                    }
                }

            ViewBag.ErrMessage = "Error: captcha is not valid.";
            return View(usr);
        }

        public ActionResult LoginFailed()
        {
            ViewBag.LoginFailed = "You are authorize to this activity.";
            return View();
        }

    }
}