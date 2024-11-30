
using DocumentDomain.Entities;

namespace DocumentDomain.Interfaces
{
    public interface IDocumentRepository
    {
       Task<string> processDocument(FormatRequest Xml);
    }
}
