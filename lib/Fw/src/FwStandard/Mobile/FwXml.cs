using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
using System.Net;

namespace FwStandard.Mobile
{
    public class FwXml
    {
        //---------------------------------------------------------------------------------------------
        public static T DeserializeString<T>(string xml)
        {
            XmlSerializer serializer;
            StringReader reader;
            T result;

            serializer = new XmlSerializer(typeof(T));
            reader     = new StringReader(xml);
            result     = (T)serializer.Deserialize(reader);
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static T DeserializeUrl<T>(string url)
        {
            string xml;
            T result;

            xml    = RequestUrl(url);
            result = (T)DeserializeString<T>(xml);
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static T DeserializeFile<T>(string filename)
        {
            string xml;
            T result;

            xml    = FileToString(filename);
            result = (T)DeserializeString<T>(xml);
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string Serialize<T>(T obj)
        {
            XmlSerializer serializer;
            StringBuilder sb;
            string result;

            serializer = new XmlSerializer(typeof(T));
            sb         = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, obj);
            }
            result = sb.ToString();
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string SerializeCleanXml<T>(T obj)
        {
            XmlSerializer serializer;
            StringBuilder sb;
            XmlSerializerNamespaces namespaces;
            XmlWriterSettings xmlWriterSettings;
            string result;

            serializer = new XmlSerializer(typeof(T));
            sb         = new StringBuilder();
            namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true;
            xmlWriterSettings.Encoding           = Encoding.ASCII;
            xmlWriterSettings.Indent             = false;
            xmlWriterSettings.ConformanceLevel   = ConformanceLevel.Document;
            using (XmlWriter xmlWriter = XmlWriter.Create(sb, xmlWriterSettings))
            {
                serializer.Serialize(xmlWriter, obj, namespaces);
            }
            result = sb.ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static Dictionary<string, string> GetDictionary(string xml)
        {
            StreamReader streamReader;
            StringReader stringReader;
            XmlTextReader xmlTextReader;
            Dictionary<string, string> parameters;
            string elementName;
            
            streamReader  = null;
            stringReader  = null;
            xmlTextReader = null;
            parameters    = new Dictionary<string, string>();
            elementName   = string.Empty;
            try
            {
                stringReader  = new StringReader(xml);
                xmlTextReader = new XmlTextReader(stringReader);
                while (xmlTextReader.Read())
                {
                    switch (xmlTextReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            elementName = xmlTextReader.Name.ToLower();
                            parameters[elementName] = string.Empty;
                            break;
                        case XmlNodeType.Text:
                            if (elementName == string.Empty)
                            {
                                throw new ApplicationException("Text node: '" + xmlTextReader.Value + "' must be contained within an element.");
                            }
                            parameters[elementName] = xmlTextReader.Value;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Xml Parsing error. " + ex.Message);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                }
                if (xmlTextReader != null)
                {
                    xmlTextReader.Close();
                }
            }
            return parameters;
        }
        //---------------------------------------------------------------------------------------------
        private static string RequestUrl(string url)
        {
            Encoding enc;
            HttpWebRequest request;
            HttpWebResponse response    = null;
            StreamReader responseStream = null;
            string html;

            if (string.IsNullOrEmpty(url)) throw new ArgumentException("Parameter url cannot be null or empty.");
            html = string.Empty;
            try
            {
                enc            = Encoding.GetEncoding(1252);
                request        = (HttpWebRequest)WebRequest.Create(url);
                response       = (HttpWebResponse)request.GetResponse();
                responseStream = new StreamReader(response.GetResponseStream(), enc);
                html           = responseStream.ReadToEnd();
            }
            catch
            {
                // ignore exceptions
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }

            return html;
        }
        //---------------------------------------------------------------------------------------------
        public static string FileToString(string path)
        {
            string fileText;

            fileText = File.ReadAllText(path);
            
            return fileText;
        }
        //---------------------------------------------------------------------------------------------
    }
}
