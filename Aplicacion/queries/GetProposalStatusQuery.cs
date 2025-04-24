using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Aplicacion.queries
{
    class GetProposalStatusQuery : IRequest<string>
    {
        public int ProposalId { get; set; }
    }
}
