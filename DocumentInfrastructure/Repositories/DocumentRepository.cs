using DocumentDomain.Interfaces;
using DocumentDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DocumentInfrastructure.Repositories
{
    public class DocumentRepository: IDocumentRepository
    {
        private readonly IFormat _providerFormat;
        private readonly IXmlValidation _providerValidation;
        private readonly IJsonBuild _providerJson;
        public DocumentRepository(
            IFormat providerFormat,
            IXmlValidation providerValidation,
            IJsonBuild providerJson)
        {
            _providerFormat = providerFormat;
            _providerValidation = providerValidation;
            _providerJson = providerJson;
        }

        public async Task<string> processDocument(FormatRequest request)
        {
            string? strXml = request.Xml;

            string decodeXml = _providerFormat.Base64Decode(strXml);
            string xmlDocument =  _providerValidation.XmlDocumentValidate(decodeXml);
            var result = await _providerJson.CreateJson(xmlDocument);

           
            return result; 
        }
    }
}
