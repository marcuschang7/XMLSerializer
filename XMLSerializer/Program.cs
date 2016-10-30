using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace XMLSerializer
{
    public class Parameters
    {
        private string parameter;

        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

    }
    public class XMLJobs
    {
        public string procType { get; set; }
        public Parameters parameters;
        public DateTime DOB { set; get; }
        public Double Gross_Salary { set; get; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            XMLJobs XMLJob = new XMLJobs();
            XMLJob.parameters = new Parameters();
            XMLJob.procType = "Reset IIS";
            XMLJob.parameters.Parameter = "I am parameter";

            string strView = CreateXML(XMLJob); //Object to XML

            // Back to object
            XMLJobs objback2object = new XMLJobs();
            XMLJob.parameters = new Parameters();
            objback2object = (XMLJobs)CreateObject(strView, objback2object);
        }
        /// <summary>
        /// Create XML from object
        /// </summary>
        /// <param name="YourClassObject"></param>
        /// <returns></returns>
        public static string CreateXML(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
        /// <summary>
        /// Convert back to object
        /// </summary>
        /// <param name="XMLString"></param>
        /// <param name="YourClassObject"></param>
        /// <returns></returns>
        public static Object CreateObject(string XMLString, Object YourClassObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(YourClassObject.GetType());
            //The StringReader will be the stream holder for the existing XML file 
            YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially deserialized, the data is represented by an object without a defined type 
            return YourClassObject;
        }

    }
}
