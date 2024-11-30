namespace DocumentInfrastructure.ErrorHandlers
{

    public class InvalidXmlDocumentException : Exception
    {
        public InvalidXmlDocumentException() : base() { }
        public InvalidXmlDocumentException(string message) : base(message) { }
        public InvalidXmlDocumentException(string message, Exception inner) : base(message, inner) { }
    }
}
