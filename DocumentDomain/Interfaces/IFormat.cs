using DocumentDomain.Entities;

namespace DocumentDomain.Interfaces
{
    public interface IFormat
    {
        public string Base64Encode(string plainText);
        public string Base64Decode(string base64EncodedData);
    }
}
