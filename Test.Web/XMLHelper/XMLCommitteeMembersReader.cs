using SparshWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SparshWeb.XMLHelper
{
    public static class XMLCommitteeMembersReader
    {
        //static string xmlPath = @"D:\Amit\TestProj\Test.Web\Test.Web\XMLDB\CommitteeMembersDB.xml";
        static string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~\\XMLDB\\CommitteeMembersDB.xml");

        public static List<CommitteeMemberViewModel> CommitteeMemberDataList()
        {
            List<CommitteeMemberViewModel> list = new List<CommitteeMemberViewModel>();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach (XmlNode node in doc.GetElementsByTagName("Member"))
            {
                CommitteeMemberViewModel model = new CommitteeMemberViewModel();
                model.MemderId = Convert.ToInt32(node.ChildNodes[0].InnerText);
                model.Title = node.ChildNodes[1].InnerText;
                model.FirstName = node.ChildNodes[2].InnerText;
                model.MiddleName = node.ChildNodes[3].InnerText;
                model.LastName = node.ChildNodes[4].InnerText;
                model.MemberFlat = node.ChildNodes[5].InnerText;
                model.Contact = node.ChildNodes[6].InnerText;
                model.WhatsUp = node.ChildNodes[7].InnerText;
                model.IsActive = Convert.ToBoolean(node.ChildNodes[8].InnerText);
                model.MemberGroup = node.ChildNodes[9].InnerText;
                list.Add(model);
            }
            return list;
        }

        public static bool AddCommitteeMember(CommitteeMemberViewModel model)
        {
            try
            {
                string active = model.IsActive == true ? "True" : "False";
                XElement xml = XElement.Load(xmlPath);
                xml.Add(new XElement("Member",
                new XElement("MemebrId", model.MemderId),
                new XElement("MemberTitle", model.Title),
                new XElement("MemberFirstName", model.FirstName),
                new XElement("MemberMiddleName", model.MiddleName),
                new XElement("MemberLastName", model.LastName),
                new XElement("MemberFlat", model.MemberFlat),
                new XElement("MemberContact", model.Contact),
                new XElement("MemberWhatsUp", model.WhatsUp),
                new XElement("IsActive", active),
                new XElement("MemberGroup", model.MemberGroup)));
                xml.Save(xmlPath);

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool UpdateCommitteeMemberData(CommitteeMemberViewModel model)
        {
            try
            {
                var xmlDoc = XElement.Load(xmlPath);
                var items = from item in xmlDoc.Descendants("Member")
                            where item.Element("MemebrId").Value == model.MemderId.ToString()
                            select item;

                foreach (XElement itemElement in items)
                {
                    itemElement.SetElementValue("MemberTitle", model.Title);
                    itemElement.SetElementValue("MemberFirstName", model.FirstName);
                    itemElement.SetElementValue("MemberMiddleName", model.MiddleName);
                    itemElement.SetElementValue("MemberLastName", model.LastName);
                    itemElement.SetElementValue("MemberFlat", model.MemberFlat);
                    itemElement.SetElementValue("MemberContact", model.Contact);
                    itemElement.SetElementValue("MemberWhatsUp", model.WhatsUp);                    
                    itemElement.SetElementValue("IsActive", model.IsActive.ToString());
                    itemElement.SetElementValue("MemberGroup", model.MemberGroup);
                }

                xmlDoc.Save(xmlPath);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool DeleteCommitteeMemberData(CommitteeMemberViewModel model)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode node = xmlDoc.SelectSingleNode("/CommitteeMembers/Member[MemebrId=" + model.MemderId + "]");
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

    }
}