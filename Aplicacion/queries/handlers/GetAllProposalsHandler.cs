using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Dominio.repositorios;
using MediatR;

namespace Aplicacion.queries.handlers
{
    class GetAllProposalsHandler : IRequestHandler<GetAllProposalsQuery, List<ProposalSummaryDto>>
    {
        private readonly IProjectProposalRepository _proposalRepo;

        public GetAllProposalsHandler(IProjectProposalRepository proposalRepo)
        {
            _proposalRepo = proposalRepo;
        }

        public async Task<List<ProposalSummaryDto>> Handle(GetAllProposalsQuery request, CancellationToken cancellationToken)
        {
            var proposals = await _proposalRepo.ObtenerTodosAsync();
            return proposals.Select(p => new ProposalSummaryDto
            {
                Id = p.Id,
                Title = p.Title,
                Status = p.StatusNavigation.Name
            }).ToList();
        }
    }
}
