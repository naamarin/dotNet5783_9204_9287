using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalXml;

internal static class Config
{
    static string s_config = "configuration";
    public static int NextOrderNumber()
    {
        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config),"NextOrderID") ?? 0;
        //XElement configRoot =XElement.Load(s_config);
        //int.TryParse(configRoot.Element("NextOrderID").Value, out int id);
        //return id;
    }
    public static void premotOrderNumber(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderID")?.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }


    public static int NextOrderItemNumber()
    {
        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config),"NextOrderItemID") ?? 0;
    }
    public static void premotOrderItemNumber(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItemID")?.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }

}
