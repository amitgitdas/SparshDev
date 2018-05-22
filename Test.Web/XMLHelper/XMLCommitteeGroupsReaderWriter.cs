using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using SparshWeb.Models;

namespace SparshWeb.XMLHelper
{
    public static class XMLCommitteeGroupsReaderWriter
    {
        //static string xmlPath = @"D:\Amit\TestProj\Test.Web\Test.Web\XMLDB\CommitteeGroupDB.xml";
        static string xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~\\XMLDB\\CommitteeGroupDB.xml");

        public static List<CommitteeGroupViewModel> CommitteeGroupData()
        {
            List<CommitteeGroupViewModel> list = new List<CommitteeGroupViewModel>();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            foreach (XmlNode node in doc.GetElementsByTagName("Group"))
            {
                CommitteeGroupViewModel model = new CommitteeGroupViewModel();
                model.GroupId = Convert.ToInt32(node.ChildNodes[0].InnerText);
                model.GroupName = node.ChildNodes[1].InnerText;
                model.GroupOrder = Convert.ToInt32(node.ChildNodes[2].InnerText);
                model.IsActive = Convert.ToBoolean(node.ChildNodes[3].InnerText);
                list.Add(model);
            }
            return list;
        }

        public static bool AddCommitteeGroupData(CommitteeGroupViewModel model)
        {
            try
            {
                XElement xml = XElement.Load(xmlPath);
                xml.Add(new XElement("Group",
                new XElement("GroupId", model.GroupId),
                new XElement("GroupName", model.GroupName),
                new XElement("GroupOrder", model.GroupOrder),
                new XElement("IsActive", model.IsActive)));
                xml.Save(xmlPath);

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool UpdateCommitteeGroupData(CommitteeGroupViewModel model)
        {
            try
            {
                var xmlDoc = XElement.Load(xmlPath);
                var items = from item in xmlDoc.Descendants("Group")
                            where item.Element("GroupId").Value == model.GroupId.ToString()
                            select item;

                foreach (XElement itemElement in items)
                {
                    itemElement.SetElementValue("GroupName", model.GroupName);
                    itemElement.SetElementValue("GroupOrder", model.GroupOrder.ToString());
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

        public static bool DeleteCommitteeGroupData(CommitteeGroupViewModel model)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode node = xmlDoc.SelectSingleNode("/CommitteeGroups/Group[GroupId=" + model.GroupId + "]");
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