using SparshWeb.ModelBusiness;
using SparshWeb.Models;
using SparshWeb.XMLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Web.ModelBusiness
{
    public static class CommitteeGroupsBusiness
    {
        public static List<CommitteeGroupViewModel> GetAllCommitteeGroupList()
        {
            List<CommitteeGroupViewModel> cgList = XMLCommitteeGroupsReaderWriter.CommitteeGroupData();
            return cgList;
        }

        public static CommitteeGroupViewModel DetailsCommitteeGroup(int groupId)
        {
            CommitteeGroupViewModel model = XMLCommitteeGroupsReaderWriter.CommitteeGroupData().Where(x => x.GroupId == groupId).FirstOrDefault();
            return model;
        }

        public static bool AddCommitteeGroup(CommitteeGroupViewModel model)
        {
            return XMLCommitteeGroupsReaderWriter.AddCommitteeGroupData(model);
        }

        public static bool UpdateCommitteeGroup(CommitteeGroupViewModel model)
        {
            return XMLCommitteeGroupsReaderWriter.UpdateCommitteeGroupData(model);
        }

        public static bool DeleteCommitteeGroup(CommitteeGroupViewModel model)
        {
            return XMLCommitteeGroupsReaderWriter.DeleteCommitteeGroupData(model);
        }

        public static int NewGroupId()
        {
            int id = XMLCommitteeGroupsReaderWriter.CommitteeGroupData().OrderByDescending(x => x.GroupId).Select(x => x.GroupId).FirstOrDefault();
            id = id + 1;
            return id;
        }

        public static bool IsGroupAssociatedWithMenber(string groupName)
        {
            int cnt = CommitteeMemberBusiness.GetAllCommitteeMemberList().Where(x => x.MemberGroup.ToUpper().Trim() == groupName.ToUpper().Trim()).Count();
            if (cnt > 0)
                return true;
            else
                return false;
        }
    }
}