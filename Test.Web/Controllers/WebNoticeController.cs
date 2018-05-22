using SparshWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using SparshWeb.XMLHelper;
using SparshWeb.ModelBusiness;
using SparshWeb.CustomAttribute;

namespace Test.Web.Controllers
{
    public class WebNoticeController : Controller
    {
        // GET: WebNotice
        [CustomAuthentication]
        public ActionResult Index(int? page)
        {
            List<WebNoticeViewModel> wnList = XMLWebNoticeReaderWriter.ReadWebNoticeData();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            wnList = wnList.OrderByDescending(x => x.NoticeId).ToList();
            return View(wnList.ToPagedList(pageNumber, pageSize));
        }

        // GET: WebNotice/Details/5
        [CustomAuthentication]
        public ActionResult Details(int id)
        {
            WebNoticeViewModel model = new WebNoticeViewModel();            
            model = WebNoticeBusiness.GetWebNoticebyId(id);
            return View(model);
        }

        // GET: WebNotice/Create
        [CustomAuthentication]
        public ActionResult Create()
        {
            WebNoticeViewModel model = new WebNoticeViewModel();
            model.PublishedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            return View(model);
        }

        // POST: WebNotice/Create
        [HttpPost]
        [CustomAuthentication]
        public ActionResult Create(WebNoticeViewModel model)
        {
            try
            {
                if(ModelState.IsValid && WebNoticeBusiness.IsAnyActiveWebNotice() <= 0 )
                {
                    int lastId = WebNoticeBusiness.LastNoticeId();
                    model.NoticeId = lastId + 1;
                    model.UpdatedDate = DateTime.Now;
                    WebNoticeBusiness.ApendWebNotice(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    //ModelState.AddModelError("IsActive", "Already Active Notice is there, Make all other notice inactive.");
                    ViewBag.ErrorMsg = "Already Active Notice is there, Make all other notice inactive and update this.";
                    return View(model);
                }
            }
            catch
            {
                //ModelState.AddModelError("IsActive", "Already Active Notice is there, Make all other notice inactive.");
                //return RedirectToAction("Create");
            }
            return View(model);
        }

        // GET: WebNotice/Edit/5
        [CustomAuthentication]
        public ActionResult Edit(int id)
        {
            WebNoticeViewModel model = XMLWebNoticeReaderWriter.GetWebNoticeDetails(id);
            return View(model);
        }

        // POST: WebNotice/Edit/5
        [HttpPost]
        [CustomAuthentication]
        public ActionResult Edit(WebNoticeViewModel model)
        {
            try
            {
                model.UpdatedDate = DateTime.Now;
                bool result = XMLWebNoticeReaderWriter.UpdateWebNotice(model);
                if(result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }

                
            }
            catch
            {
                
            }
            return View();
        }

        // GET: WebNotice/Delete/5
        [HttpGet]
        [CustomAuthentication]
        public ActionResult Delete(int id)
        {
            WebNoticeViewModel model = new WebNoticeViewModel();
            model = WebNoticeBusiness.GetWebNoticebyId(id);
            return View(model);
        }

        // POST: WebNotice/Delete/5
        [HttpPost, ActionName("Delete")]
        [CustomAuthentication]
        public ActionResult DeleteConformed(int id)
        {
            try
            {
                WebNoticeViewModel model = new WebNoticeViewModel();
                model = WebNoticeBusiness.GetWebNoticebyId(id);
                //model.NoticeId = 1013;
                WebNoticeBusiness.DeleteWebNotice(model);
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
