using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Library.Net.CLCommon
{
    /// <summary>
    /// XML文件操作类
    /// </summary>
    public class CXml
    {
        /// <summary>
        /// 判断节点是否存在
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public static bool IsExistXmlNode(string xmlPath, string xmlName, string nodeName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath + "\\" + xmlName + ".xml");
                XmlNode root = xmlDoc.DocumentElement;
                XmlNode node = null;
                SearchXmlNode(root, nodeName, ref node);
                if (node == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="xmlRootName">xml根节点名称</param>
        /// <returns></returns>
        public static void CreateXmlFile(string xmlPath, string xmlName, string xmlRootName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                //加入XML的声明段落
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                //加入根元素
                XmlElement xmlElem = xmlDoc.CreateElement(xmlRootName);
                xmlDoc.AppendChild(xmlElem);
                xmlDoc.Save(xmlPath + "\\" + xmlName + ".xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 插入xml节点
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodeValue">节点值</param>
        /// <returns></returns>
        public static void InsertXmlNode(string xmlPath, string xmlName, string nodeName, string nodeValue)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath + "\\" + xmlName + ".xml");
                XmlElement root = xmlDoc.DocumentElement;
                XmlNode node = xmlDoc.CreateElement(nodeName);
                node.InnerText = nodeValue;
                root.AppendChild(node);
                xmlDoc.Save(xmlPath + "\\" + xmlName + ".xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据xml节点名插入子节点
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="subNodeName">子节点名</param>
        /// <param name="subNodeValue">子节点值</param>
        public static void InsertSubXmlNode(string xmlPath, string xmlName, string nodeName, string subNodeName, string subNodeValue)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath + "\\" + xmlName + ".xml");
                XmlNode root = xmlDoc.DocumentElement;
                XmlNode node = null;
                SearchXmlNode(root, nodeName, ref node);
                XmlNode subNode = xmlDoc.CreateElement(subNodeName);
                subNode.InnerText = subNodeValue;
                node.AppendChild(subNode);
                xmlDoc.Save(xmlPath + "\\" + xmlName + ".xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据xml节点名设置节点的属性
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="attName">属性名</param>
        /// <param name="attValue">属性值</param>
        public static void SetXmlNodeAttribute(string xmlPath, string xmlName, string nodeName, string attName, string attValue)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath + "\\" + xmlName + ".xml");
                XmlNode root = xmlDoc.DocumentElement;
                XmlNode node = null;
                SearchXmlNode(root, nodeName, ref node);
                (node as XmlElement).SetAttribute(attName, attValue);
                xmlDoc.Save(xmlPath + "\\" + xmlName + ".xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		/// <summary>
		/// 根据xml节点名获取节点的属性<para/>
		/// <returns>未找到节点返回"NoNode",不存在该属性返回"NoAttribute"</returns>
		/// </summary>
		/// <param name="xmlPath">xml文件路径</param>
		/// <param name="xmlName">xml文件名</param>
		/// <param name="nodeName">节点名</param>
		/// <param name="attName">属性名</param>
		public static string GetXmlNodeAttribute(string xmlPath, string xmlName, string nodeName, string attName)
		{
			string result = "";
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlPath + "\\" + xmlName + ".xml");
				XmlNode root = xmlDoc.DocumentElement;
				XmlNode node = root.SelectSingleNode(nodeName);
				if (node == null)//未找到节点
					result = "NoNode";
				else
				{
					XmlAttribute attr = node.Attributes[attName];
					if (attr == null)//不存在该属性
						result = "NoAttribute";
					else
						result = attr.Value;
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return result;
		}
        /// <summary>
        /// 根据xml节点名修改节点值
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodeValue">节点值</param>
        public static void ModifyXmlNode(string xmlPath, string xmlName, string nodeName, string nodeValue)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath + "\\" + xmlName + ".xml");
                XmlNode root = xmlDoc.DocumentElement;
                XmlNode node=null;
                SearchXmlNode(root, nodeName, ref node);
                node.InnerText = nodeValue;
                xmlDoc.Save(xmlPath + "\\" + xmlName + ".xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据xml节点名删除节点
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="nodeName">节点名</param>
        public static void DeleteXmlNode(string xmlPath, string xmlName, string nodeName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath + "\\" + xmlName + ".xml");
                XmlNode root = xmlDoc.DocumentElement;
                XmlNode node = null;
                SearchXmlNode(root, nodeName, ref node);
                node.ParentNode.RemoveChild(node);
                xmlDoc.Save(xmlPath + "\\" + xmlName + ".xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 查找xml节点
        /// </summary>
        /// <param name="node">查询节点的父级</param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        private static void SearchXmlNode(XmlNode node, string nodeName, ref XmlNode serarchNode)
        {
            try
            {
                if (node.HasChildNodes)
                {
                    serarchNode = node.SelectSingleNode(nodeName);
                    if (serarchNode == null)
                    {
                        foreach (XmlNode subNode in node.ChildNodes)
                        {
                            SearchXmlNode(subNode, nodeName, ref serarchNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
