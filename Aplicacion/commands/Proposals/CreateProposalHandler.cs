using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.services;
using Dominio.entidades;
using Dominio.repositorios;
using MediatR;

namespace Aplicacion.commands.Proposals
{
    class CreateProposalHandler : IRequestHandler<CreateProposalCommand, int>
    {
        private readonly IProjectProposalRepository _proposalRepo;
        private readonly IApprovalWorkflowService _workflowService;

        public CreateProposalHandler(
            IProjectProposalRepository proposalRepo,
            IApprovalWorkflowService workflowService)
        {
            _proposalRepo = proposalRepo;
            _workflowService = workflowService;
        }

        public async Task<int> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            var proposal = new ProjectProposal
            {
                Title = request.Title,
                Description = request.Description,
                Area = request.Area,
                type = request.Type,
                EstimatedAmount = request.EstimatedAmount,
                EstimatedDuration = request.EstimatedDuration,
                CreatedBy = request.CreatedBy,
                CreateAt = DateTime.UtcNow,
                Status = 1 // Pendiente
            };

            await _proposalRepo.CrearAsync(proposal);

            await _workflowService.GenerateApprovalWorkflow(proposal);

            return proposal.Id;
        }
    }
}
