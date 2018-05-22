using SparshWeb.ModelBusiness;
using SparshWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Test.Web.ModelBusiness;
using SparshWeb.CustomAttribute;

namespace SparshWeb.Controllers
{
    public class CommitteeMemberController : Controller
    {
        // GET: CommitteeMember
        [CustomAuthentication]
        public ActionResult Index(int? page)
        {
            List<CommitteeMemberViewModel> list = CommitteeMemberBusiness.GetAllCommitteeMemberList();
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: CommitteeMember/Details/5
        [CustomAuthentication]
        public ActionResult Details(int id)
        {
            CommitteeMemberViewModel model = CommitteeMemberBusiness.CommitteeMemberDetails(id);
            return View(model);
        }

        // GET: CommitteeMember/Create
        [CustomAuthentication]
        public ActionResult Create()
        {
            CommitteeMemberViewModel model = new CommitteeMemberViewModel();
            ViewBag.MemberTypeList = new SelectList(CommitteeGroupsBusiness.GetAllCommitteeGroupList(), "GroupId", "GroupName");
            model.Contact = "+91 0000000000";
            model.WhatsUp = "+91 0000000000";
            return View(model);
        }

        // POST: CommitteeMember/Create
        [HttpPost]
        [CustomAuthentication]
        public ActionResult Create(CommitteeMemberViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                bool result = CommitteeMemberBusiness.AddCommitteeMember(model);
                if(result)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Create", model);
            }
            catch
            {
                return View();
            }
        }

        // GET: CommitteeMember/Edit/5
        [CustomAuthentication]
        public ActionResult Edit(int id)
        {
            CommitteeMemberViewModel model = CommitteeMemberBusiness.CommitteeMemberDetails(id);
            List<CommitteeGroupViewModel> grList = CommitteeGroupsBusiness.GetAllCommitteeGroupList();
            CommitteeGroupViewModel gmodel = CommitteeGroupsBusiness.GetAllCommitteeGroupList().Where(x => x.GroupName == model.MemberGroup).FirstOrDefault();
            model.MemberGroupId = gmodel.GroupId;
            model.NameTitleList = CommitteeMemberBusiness.TitleList();
            var tt = new SelectList(model.NameTitleList, "Name", "Desc", model.Title);            
            var list = new SelectList(grList, "GroupId", "GroupName", gmodel.GroupId);
            ViewBag.MemberTypeList = list;
            ViewBag.TitleList = tt;
            return View(model);
        }

        // POST: CommitteeMember/Edit/5
        [HttpPost]
        [CustomAuthentication]
        public ActionResult Edit(CommitteeMemberViewModel model)
        {
            try
            {
                if(ModelState.IsValid == true && model != null && model.MemderId>0)
                {
                    bool result = CommitteeMemberBusiness.UpdateCommitteeMember(model);
                    if(result)
                        return RedirectToAction("Index");
                    else
                        return View(model);
                }
            }
            catch
            {
                return View();
            }
            return View(model);
        }

        // GET: CommitteeMember/Delete/5
        [CustomAuthentication]
        public ActionResult Delete(int id)
        {
            CommitteeMemberViewModel model = CommitteeMemberBusiness.CommitteeMemberDetails(id);
            return View(model);
        }

        // POST: CommitteeMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [CustomAuthentication]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                CommitteeMemberViewModel model = CommitteeMemberBusiness.CommitteeMemberDetails(id);
                if (model != null && model.MemderId > 0)
                {
                    bool result = CommitteeMemberBusiness.DeleteCommitteeMember(model);
                    if (result)
                        return RedirectToAction("Index");
                    else
                        return View(model);
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }
    }
}
