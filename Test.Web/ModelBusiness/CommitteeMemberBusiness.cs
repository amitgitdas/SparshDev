using SparshWeb.Models;
using SparshWeb.XMLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Web.ModelBusiness;

namespace SparshWeb.ModelBusiness
{
    public static class CommitteeMemberBusiness
    {
        public static List<CommitteeMemberViewModel> GetAllCommitteeMemberList()
        {
            List<CommitteeMemberViewModel> cgList = XMLCommitteeMembersReader.CommitteeMemberDataList();
            return cgList;
        }

        public static List<DisplayGroupMember> DisplayGroupMembers()
        {
            List<DisplayGroupMember> gmList = new List<DisplayGroupMember>();

            var gList = CommitteeGroupsBusiness.GetAllCommitteeGroupList().Where(x => x.IsActive == true).ToList();
            
            foreach (var item in gList)
            {
                DisplayGroupMember model = new DisplayGroupMember();
                model.GroupId = item.GroupId;
                model.GroupName = item.GroupName;
                model.GroupOrder = item.GroupOrder;
                model.IsActive = item.IsActive;
                model.MemberList = GetAllCommitteeMemberList().Where(x => x.IsActive == true && x.MemberGroup == item.GroupName).ToList();
                gmList.Add(model);
            }
            gmList = gmList.OrderBy(x => x.GroupOrder).ToList();
            return gmList;
        }

        public static CommitteeMemberViewModel CommitteeMemberDetails(int Id)
        {
            CommitteeMemberViewModel member = XMLCommitteeMembersReader.CommitteeMemberDataList().Where(x => x.MemderId == Id).FirstOrDefault();
            return member;
        }

        public static bool AddCommitteeMember(CommitteeMemberViewModel model)
        {
            int lastId = GetAllCommitteeMemberList().OrderByDescending(x => x.MemderId).Select(x => x.MemderId).FirstOrDefault();
            model.MemderId = lastId + 1;
            model.MiddleName = string.IsNullOrEmpty(model.MiddleName) == true ? " " : model.MiddleName;
            model.MemberGroup = XMLCommitteeGroupsReaderWriter.CommitteeGroupData().Where(x => x.GroupId == model.MemberGroupId).Select(x => x.GroupName).FirstOrDefault();
            return XMLCommitteeMembersReader.AddCommitteeMember(model);
        }

        public static bool UpdateCommitteeMember(CommitteeMemberViewModel model)
        {
            model.MemberGroup = XMLCommitteeGroupsReaderWriter.CommitteeGroupData().Where(x => x.GroupId == model.MemberGroupId).Select(x => x.GroupName).FirstOrDefault();
            return XMLCommitteeMembersReader.UpdateCommitteeMemberData(model);
        }

        public static bool DeleteCommitteeMember(CommitteeMemberViewModel model)
        {
            return XMLCommitteeMembersReader.DeleteCommitteeMemberData(model);
        }

        public static List<NameTitle> TitleList()
        {
            List<NameTitle> titleList = new List<NameTitle>();
            titleList.Add(new NameTitle { Name = "Mr", Desc = "Mr" });
            titleList.Add(new NameTitle { Name = "Mrs", Desc = "Mrs" });
            titleList.Add(new NameTitle { Name = "Dr", Desc = "Dr" });
            return titleList;
        }
    }
}