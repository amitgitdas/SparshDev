using SparshWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SparshWeb.XMLHelper;

namespace SparshWeb.ModelBusiness
{
    public static class WebNoticeBusiness
    {
        public static List<WebNoticeViewModel> GetAllWebNoticeList()
        {
            List<WebNoticeViewModel> wnList = XMLWebNoticeReaderWriter.ReadWebNoticeData();
            return wnList;
        }

        public static WebNoticeViewModel GetWebNoticebyId(int Id)
        {
            WebNoticeViewModel model = new WebNoticeViewModel();
            List<WebNoticeViewModel> wnList = XMLWebNoticeReaderWriter.ReadWebNoticeData();
            model = wnList.Where(x => x.NoticeId == Id).FirstOrDefault();
            return model;
        }

        public static int LastNoticeId()
        {
            int Id = GetAllWebNoticeList().OrderByDescending(x => x.NoticeId).FirstOrDefault().NoticeId;
            return Id;
        }

        public static int IsAnyActiveWebNotice()
        {
            int cnt = GetAllWebNoticeList().Where(x => x.IsActive == true).Count();
            return cnt;
        }

        public static bool ApendWebNotice(WebNoticeViewModel model)
        {
            return XMLWebNoticeReaderWriter.AddWebNotice(model);
        }

        public static bool UpdateWebNotice(WebNoticeViewModel model)
        {
            return XMLWebNoticeReaderWriter.UpdateWebNotice(model);
        }

        public static bool DeleteWebNotice(WebNoticeViewModel model)
        {
            return XMLWebNoticeReaderWriter.DeleteWebNotice(model);
        }

        public static string DisplayNoticeBoard()
        {
            string display = GetAllWebNoticeList().Where(x => x.IsActive ).Select(x => x.NoticeDesc).FirstOrDefault();
            return display;
        }
    }
}