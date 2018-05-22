using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SparshWeb.Models
{
    public class CommitteeMemberViewModel
    {
        public int MemderId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MemberFlat { get; set; }
        public string Contact { get; set; }
        public string WhatsUp { get; set; }
        public bool IsActive { get; set; }
        public string MemberGroup { get; set; }
        public int MemberGroupId { get; set; }
        public List<NameTitle> NameTitleList { get; set; }

        [Display(Name = "Name")]
        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(MiddleName))
                {
                    return Title + ". " + FirstName + " " + MiddleName + " " + LastName;
                }
                else
                {
                    return Title + ". " + FirstName + " " + LastName;
                }
            }
        }
        
    }

    public class NameTitle
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}