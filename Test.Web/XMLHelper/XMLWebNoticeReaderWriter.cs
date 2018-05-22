using SparshWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SparshWeb.XMLHelper
{
    public static class XMLWebNoticeReaderWriter
    {
        //static string xmlPath = @"D:\Amit\TestProj\Test.Web\Test.Web\XMLDB\WebNoticeDB.xml";
        static string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~\\XMLDB\\WebNoticeDB.xml");

        public static List<WebNoticeViewModel> ReadWebNoticeData()
        {
            List<WebNoticeViewModel> list = new List<WebNoticeViewModel>();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach (XmlNode node in doc.GetElementsByTagName("webnotice"))
            {
                WebNoticeViewModel model = new WebNoticeViewModel();
                model.NoticeId = Convert.ToInt32(node.ChildNodes[0].InnerText);
                model.NoticeDesc = node.ChildNodes[1].InnerText;
                model.PublishedDate = Convert.ToDateTime(node.ChildNodes[2].InnerText);
                model.UpdatedDate = Convert.ToDateTime(node.ChildNodes[3].InnerText);
                model.IsActive = Convert.ToBoolean(node.ChildNodes[4].InnerText);
                list.Add(model);             
            }
            return list;
        }

        public static bool AddWebNotice(WebNoticeViewModel model)
        {
            try
            {
                XElement xml = XElement.Load(xmlPath);
                xml.Add(new XElement("webnotice",
                new XElement("NoticeId", model.NoticeId),
                new XElement("NoticeDesc", model.NoticeDesc),
                new XElement("PublishedDate", model.PublishedDate),
                new XElement("UpdatedDate", model.UpdatedDate),
                new XElement("IsActive", model.IsActive)));
                xml.Save(xmlPath);

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool DeleteWebNotice(WebNoticeViewModel model)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode node = xmlDoc.SelectSingleNode("/webnotices/webnotice[NoticeId=" + model.NoticeId + "]");
                if (node != null)
                {
                    node.ParentNode.RemoveChild(node);
                }
                xmlDoc.Save(xmlPath);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool UpdateWebNotice(WebNoticeViewModel model)
        {
            try
            {
                var xmlDoc = XElement.Load(xmlPath);
                var items = from item in xmlDoc.Descendants("webnotice")
                            where item.Element("NoticeId").Value == model.NoticeId.ToString()
                            select item;

                foreach (XElement itemElement in items)
                {
                    itemElement.SetElementValue("NoticeId", model.NoticeId);
                    itemElement.SetElementValue("NoticeDesc", model.NoticeDesc);
                    itemElement.SetElementValue("PublishedDate", model.PublishedDate);
                    itemElement.SetElementValue("UpdatedDate", model.UpdatedDate);
                    itemElement.SetElementValue("IsActive", model.IsActive.ToString());
                }

                xmlDoc.Save(xmlPath);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static WebNoticeViewModel GetWebNoticeDetails(int id)
        {
            WebNoticeViewModel model = ReadWebNoticeData().Where(x => x.NoticeId == id).FirstOrDefault();
            return model;
        }
    }
}