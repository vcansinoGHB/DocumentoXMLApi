using DocumentDomain.Interfaces;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;
using DocumentInfrastructure.ErrorHandlers;

namespace DocumentInfrastructure.Repositories
{
    public class XmlValidationRepository : IXmlValidation
    {
        public XDocument ToXDocument(XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
        public string XmlDocumentValidate(string xml)
        {

            XmlDocument requestedDoc = new XmlDocument();
            requestedDoc.LoadXml(xml);

            XmlSchemaSet receiptSchema = new XmlSchemaSet();
            receiptSchema.Add("", "./Schemas/comprobante.xsd");

            XDocument XReqDocument = ToXDocument(requestedDoc);

            bool errors = false;
            XReqDocument.Validate(receiptSchema, (o, e) => { errors = true; });

            var responseDoc = errors ? throw new InvalidXmlDocumentException("The XML documento is incorrect") : "valido";

            return xml;
        }
    }
}
