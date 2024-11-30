using System.Xml;
using Newtonsoft.Json.Linq;


namespace DocumentDomain.Interfaces
{
    public interface IJsonBuild
    {
        public Task<string> CreateJson(string xml);
        public JArray getRecipts(IEnumerable<XmlElement> receiptQuery);
        public JArray getIssuer(XmlElement receiptElement);
        public JArray getReceiver(XmlElement receiptElement);
        public JObject getConcepts(XmlElement receiptElement);
        public JArray getTax(XmlElement receiptElement);


    }
}
