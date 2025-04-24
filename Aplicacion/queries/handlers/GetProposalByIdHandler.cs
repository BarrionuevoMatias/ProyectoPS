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
    class GetProposalByIdHandler : IRequestHandler<GetProposalByIdQuery, ProposalDto>
    {
        private readonly IProjectProposalRepository _proposalRepo;

        public GetProposalByIdHandler(IProjectProposalRepository proposalRepo)
        {
            _proposalRepo = proposalRepo;
        }

        public async Task<ProposalDto> Handle(GetProposalByIdQuery request, CancellationToken cancellationToken)
        {
            var proposal = await _proposalRepo.ObtenerPorIdAsync(request.ProposalId);
            if (proposal == null)
                throw new KeyNotFoundException("Proposal not found");

            return new ProposalDto
            {
                Id = proposal.Id,
                Title = proposal.Title,
                Description = proposal.Description,
                Area = proposal.AreaNavigation.Nombre,
                Type = proposal.TypeNavigation.Name,
                EstimatedAmount = proposal.EstimatedAmount,
                EstimatedDuration = proposal.EstimatedDuration,
                Status = proposal.StatusNavigation.Name,
                CreatedBy = proposal.CreatedByNavigation.Name,
                CreateAt = proposal.CreateAt,
                Steps = proposal.ProjectApprovalSteps.Select(s => new ApprovalStepDto
                {
                    Id = s.Id,
                    StepOrder = s.StepOrder,
                    ApproverRole = s.ApproverRole.Name,
                    Status = s.StatusNavigation.Name,
                    ApproverUser = s.ApproverUser?.Name,
                    DecisionDate = s.DecisionDate,
                    Observations = s.Observations
                }).OrderBy(s => s.StepOrder).ToList()
            };
        }
    }
}
