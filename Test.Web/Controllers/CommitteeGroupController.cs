using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SparshWeb.CustomAttribute;
using SparshWeb.Models;
using Test.Web.ModelBusiness;

namespace Test.Web.Controllers
{
    public class CommitteeGroupController : Controller
    {
        // GET: CommitteeGroup
        [CustomAuthentication]
        public ActionResult Index(int? page)
        {
            List<CommitteeGroupViewModel> cgList = CommitteeGroupsBusiness.GetAllCommitteeGroupList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(cgList.ToPagedList(pageNumber, pageSize));
        }

        // GET: CommitteeGroup/Details/5
        [CustomAuthentication]
        public ActionResult Details(int id)
        {
            CommitteeGroupViewModel model = CommitteeGroupsBusiness.DetailsCommitteeGroup(id);
            return View(model);
        }

        // GET: CommitteeGroup/Create
        [CustomAuthentication]
        public ActionResult Create()
        {
            CommitteeGroupViewModel model = new CommitteeGroupViewModel();
            return View(model);
        }

        // POST: CommitteeGroup/Create
        [HttpPost]
        [CustomAuthentication]
        public ActionResult Create(CommitteeGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.GroupId = CommitteeGroupsBusiness.NewGroupId();
                    bool result = CommitteeGroupsBusiness.AddCommitteeGroup(model);
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
            return View();
        }

        // GET: CommitteeGroup/Edit/5
        [CustomAuthentication]
        public ActionResult Edit(int id)
        {
            CommitteeGroupViewModel model = CommitteeGroupsBusiness.DetailsCommitteeGroup(id);
            return View(model);
        }

        // POST: CommitteeGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(CommitteeGroupViewModel model)
        {
            try
            {
                if(model.GroupId>0)
                {
                    CommitteeGroupsBusiness.UpdateCommitteeGroup(model);
                    return RedirectToAction("Index");
                }
                return View();
                //return RedirectToAction("Edit/Id/"+ model.GroupId);
            }
            catch
            {
                return View();
            }
        }

        // GET: CommitteeGroup/Delete/5
        [CustomAuthentication]
        public ActionResult Delete(int id)
        {
            CommitteeGroupViewModel model = CommitteeGroupsBusiness.DetailsCommitteeGroup(id);
            return View(model);
        }

        // POST: CommitteeGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {            
            try
            {
                if(id>0)
                {
                    CommitteeGroupViewModel model = CommitteeGroupsBusiness.DetailsCommitteeGroup(id);
                    bool isAssociated = CommitteeGroupsBusiness.IsGroupAssociatedWithMenber(model.GroupName);
                    if (isAssociated == false)
                    {
                        bool result = CommitteeGroupsBusiness.DeleteCommitteeGroup(model);
                        if (result)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "This Group is Associated with Some Committee Member, so you can not delete.";
                        return View(model);
                    }
                }
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
