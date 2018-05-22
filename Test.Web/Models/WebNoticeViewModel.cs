using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SparshWeb.Models
{
    public class WebNoticeViewModel
    {
        public int NoticeId { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Please Enter Notice Description.")]
        public string NoticeDesc { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}