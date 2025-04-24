using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using MediatR;

namespace Aplicacion.queries
{
    public class GetProposalByIdQuery : IRequest<ProposalDto>
    {
        public int ProposalId { get; set; }
    }
}
