using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    static readonly string s_config = "configuration";
    public static int NextOrderNumber()
    {
        return XMLTools.LoadListFromXMLElement(s_config).ToIntNullable("NextOrderID") ?? 0;
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
        return XMLTools.LoadListFromXMLElement(s_config).ToIntNullable("NextOrderItemID") ?? 0;
    }
    public static void premotOrderItemNumber(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItemID")?.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }

}
