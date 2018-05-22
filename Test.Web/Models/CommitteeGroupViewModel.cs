using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SparshWeb.Models
{
    public class CommitteeGroupViewModel
    {
        public int GroupId { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage = "Please enter Committee group name.")]
        public string GroupName { get; set; }

        //[RegularExpression("[^0-9]", ErrorMessage = "Group Order Number will numeric only")]
        public int GroupOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class DisplayGroupMember
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupOrder { get; set; }
        public bool IsActive { get; set; }
        public List<CommitteeMemberViewModel> MemberList { get; set; }
    }

    
}