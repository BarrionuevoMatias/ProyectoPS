using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.repositorios;
using MediatR;

namespace Aplicacion.queries.handlers
{
    class GetProposalStatusHandler : IRequestHandler<GetProposalStatusQuery, string>
    {
        private readonly IProjectProposalRepository _proposalRepo;

        public GetProposalStatusHandler(IProjectProposalRepository proposalRepo)
        {
            _proposalRepo = proposalRepo;
        }

        public async Task<string> Handle(GetProposalStatusQuery request, CancellationToken cancellationToken)
        {
            var proposal = await _proposalRepo.ObtenerPorIdAsync(request.ProposalId);
            return proposal.StatusNavigation.Name;
        }
    }
}
