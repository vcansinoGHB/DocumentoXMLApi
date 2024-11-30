using MediatR;
using DocumentDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentDomain.Interfaces;

namespace DocumentApplication.Commands
{
    public record processDocumentCommand(FormatRequest Xml): IRequest<string>;
    public class processDocumentCommandHandler(IDocumentRepository documentRepository): IRequestHandler<processDocumentCommand, string>
    {

        public async Task<string> Handle(processDocumentCommand request, CancellationToken cancellationToken)
        {
            return await documentRepository.processDocument(request.Xml);
        }

    }
   
}
