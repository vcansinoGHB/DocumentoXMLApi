using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace DocumentDomain.Interfaces
{
    public interface IXmlValidation
    {
        public string XmlDocumentValidate(string decodeXml);

        public XDocument ToXDocument(XmlDocument xmlDocument);
    }
}
