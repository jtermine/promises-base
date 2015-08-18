using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace Termine.Promises.Config
{
    public class PxProtectedConfigurationProvider : ProtectedConfigurationProvider
    {
        public override void Initialize(string name, NameValueCollection config)
        {
            //Environment = config["Environment"];
            //AppName = config["AppName"];
            //base.Initialize(name, config);
        }

        public PxProtectedConfigurationProvider(string environment, string appName, string sectionName = "")
        {
        }

        public override XmlNode Decrypt(XmlNode encryptedNode)
        {
            throw new NotImplementedException();

            /*
            var doc = new XmlDocument();
            var node = doc.CreateNode(XmlNodeType.Element, SectionName, "");

            //put code here to connect to the external configuration source and pull your configuration data

            var section = new StringBuilder();
            section.Append("<").Append(SectionName).Append(">");

            // Put code here to process the external configuration data and put it into a valid XML Node
            foreach (var item in configurationItems)
            {
                // Perform a check here of all child nodes of the encryptedNode which is the data inside the<EncryptedData>
                // tag of the section in the web/ app.config
                var children = encryptedNode.ChildNodes.Cast<XmlNode>();
                var childNode = children.Where(n => n.Name == item.Key).SingleOrDefault();

                if (childNode is null)
                {
                    section.Append("<add key=\"")
                        .Append(item.Name)
                        .Append("\" value=\"")
                        .Append(item.Value)
                        .Append("\" />");
                }
            else
                {
                    section.Append("<add key=\"")
                        .Append(item.Name)
                        .Append("\" value=\"")
                        .Append(childNode.Value)
                        .Append("\" />");
                }
            }
            section.Append("</").Append(SectionName).Append(">");
            doc.LoadXml(section.ToString());
            return doc.DocumentElement;
            */
        }

        public override XmlNode Encrypt(XmlNode node)
        {
            throw new NotImplementedException();
        }

    }
}