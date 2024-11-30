using DocumentDomain.Interfaces;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace DocumentInfrastructure.Repositories
{
    public class JsonBuildRepository : IJsonBuild
    {

        public async Task<string> CreateJson(string Xml)
        {
            JArray receiptList = new JArray();

            var XmlDocument = new XmlDocument();
            XmlDocument.LoadXml(Xml);

            JObject JsonDocument = new JObject();

            var receiptQuery = from p in XmlDocument.GetElementsByTagName("Comprobante").Cast<XmlElement>()
                               select p;

            receiptList = getRecipts(receiptQuery);

            JsonDocument["Comprobante"] = receiptList;

            return JsonDocument.ToString();

        }

        public JArray getTax(XmlElement receiptElement)
        {
            JObject taxItem = new JObject();
            JArray taxesList = new JArray();
            var taxQuery = from t in receiptElement.GetElementsByTagName("Impuestos").Cast<XmlElement>()
                           select t;

            foreach (XmlElement taxElement in taxQuery)
            {
                taxItem["TotalImpuestosRetenidos"] = taxElement.GetAttribute("TotalImpuestosRetenidos");
                taxItem["TotalImpuestosTrasladados"] = taxElement.GetAttribute("TotalImpuestosTrasladados");
                taxesList.Add(taxItem);
            }
            return taxesList;

        }

        public JObject getConcepts(XmlElement receiptElement)
        {
            JArray conceptsList = new JArray();
            var conceptQuery = from c in receiptElement.GetElementsByTagName("Conceptos").Cast<XmlElement>()
                               select c.ChildNodes;

            foreach (XmlNodeList conceptNode in conceptQuery)
            {
                foreach (XmlElement conceptElement in conceptNode)
                {
                    JObject conceptItem = new JObject();
                    conceptItem["NoIdentificacion"] = conceptElement.GetAttribute("NoIdentificacion");
                    conceptItem["ClaveProdServ"] = conceptElement.GetAttribute("ClaveProdServ");
                    conceptItem["Descripcion"] = conceptElement.GetAttribute("Descripcion");
                    conceptItem["ClaveUnidad"] = conceptElement.GetAttribute("ClaveUnidad");
                    conceptItem["ValorUnitario"] = conceptElement.GetAttribute("ValorUnitario");
                    conceptItem["Cantidad"] = conceptElement.GetAttribute("Cantidad");
                    conceptItem["Importe"] = conceptElement.GetAttribute("Importe");
                    conceptItem["ObjetoImp"] = conceptElement.GetAttribute("ObjetoImp");
                    conceptsList.Add(conceptItem);
                }
            }
            JObject conceptObj = new JObject();
            conceptObj["Concepto"] = conceptsList;
            return conceptObj;
        }

        public JArray getReceiver(XmlElement receiptElement)
        {
            JObject receiverItem = new JObject();
            JArray receiverList = new JArray();

            var receiverQuery = from r in receiptElement.GetElementsByTagName("Receptor").Cast<XmlElement>()
                                select r;

            foreach (XmlElement receiverElement in receiverQuery)
            {
                receiverItem["Nombre"] = receiverElement.GetAttribute("Nombre");
                receiverItem["Rfc"] = receiverElement.GetAttribute("Rfc");
                receiverList.Add(receiverItem);
            }
            return receiverList;
        }

        public JArray getIssuer (XmlElement receiptElement)
        {
            JObject issuerItem = new JObject();
            JArray issuerList = new JArray();

            var issuerQuery = from e in receiptElement.GetElementsByTagName("Emisor").Cast<XmlElement>()
                              select e;

            foreach (XmlElement issuerElement in issuerQuery)
            {
                issuerItem["Nombre"] = issuerElement.GetAttribute("Nombre");
                issuerItem["Rfc"] = issuerElement.GetAttribute("Rfc");
                issuerList.Add(issuerItem);
            }
            return issuerList;

        }

        public JArray getRecipts(IEnumerable<XmlElement> receiptQuery)
        {
            JObject receiptItem = new JObject();
            JArray receiptList = new JArray();

            foreach (XmlElement receiptElement in receiptQuery)
            {
                receiptItem["Version"] = receiptElement.GetAttribute("Version");
                receiptItem["LugarExpedicion"] = receiptElement.GetAttribute("LugarExpedicion");
                receiptItem["MetodoPago"] = receiptElement.GetAttribute("MetodoPago");
                receiptItem["TipoDeComprobante"] = receiptElement.GetAttribute("TipoDeComprobante");
                receiptItem["Folio"] = receiptElement.GetAttribute("Folio");
                receiptItem["Moneda"] = receiptElement.GetAttribute("Moneda");
                receiptItem["Serie"] = receiptElement.GetAttribute("Serie");
                receiptItem["Fecha"] = receiptElement.GetAttribute("Fecha");
                receiptItem["Total"] = receiptElement.GetAttribute("Total");
                receiptItem["SubTotal"] = receiptElement.GetAttribute("SubTotal");
               
                receiptItem["Emisor"] = getIssuer(receiptElement);
                receiptItem["Receptor"] = getReceiver(receiptElement);
                receiptItem["Conceptos"] = getConcepts(receiptElement);
                receiptItem["Impuestos"] = getTax(receiptElement);

            }
            receiptList.Add(receiptItem);

            return receiptList;

        }

    }
}
