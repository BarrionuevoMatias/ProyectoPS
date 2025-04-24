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
    class GetPendingStepsByRoleHandler : IRequestHandler<GetPendingStepsByRoleQuery, List<PendingStepDto>>
    {
        private readonly IProjectApprovalStepRepository _stepRepo;

        public GetPendingStepsByRoleHandler(IProjectApprovalStepRepository stepRepo)
        {
            _stepRepo = stepRepo;
        }

        public async Task<List<PendingStepDto>> Handle(GetPendingStepsByRoleQuery request, CancellationToken cancellationToken)
        {
            var steps = await _stepRepo.ObtenerTodosAsync();

            return steps
                .Where(s => s.ApproverRoleId == request.RoleId && s.Status == 1) // 1 = Pendiente
                .Select(s => new PendingStepDto
                {
                    StepId = s.Id,
                    ProposalId = s.ProjectProposalId,
                    ProposalTitle = s.ProjectProposal.Title,
                    StepOrder = s.StepOrder,
                    Area = s.ProjectProposal.AreaNavigation.Nombre,
                    ProjectType = s.ProjectProposal.TypeNavigation.Name,
                    Amount = s.ProjectProposal.EstimatedAmount
                })
                .OrderBy(s => s.ProposalId)
                .ThenBy(s => s.StepOrder)
                .ToList();
        }
    }
}
