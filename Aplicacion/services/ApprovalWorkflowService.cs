using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;
using Dominio.repositorios;

namespace Aplicacion.services
{
    class ApprovalWorkflowService : IApprovalWorkflowService
    {
        private readonly IApprovalRuleRepository _ruleRepo;
        private readonly IProjectApprovalStepRepository _stepRepo;

        public ApprovalWorkflowService(
            IApprovalRuleRepository ruleRepo,
            IProjectApprovalStepRepository stepRepo)
        {
            _ruleRepo = ruleRepo;
            _stepRepo = stepRepo;
        }

        public async Task GenerateApprovalWorkflow(ProjectProposal proposal)
        {
            var rules = await _ruleRepo.ObtenerTodosAsync();

            var applicableRules = rules
                .Where(r => IsRuleApplicable(r, proposal))
                .OrderBy(r => r.StepOrder)
                .ToList();

            foreach (var rule in applicableRules)
            {
                var step = new ProjectApprovalStep
                {
                    ProjectProposalId = proposal.Id,
                    ApproverRoleId = rule.ApproverRoleId.Value,
                    StepOrder = rule.StepOrder,
                    Status = 1 // Pendiente
                };

                await _stepRepo.CrearAsync(step);
            }
        }

        private bool IsRuleApplicable(ApprovalRule rule, ProjectProposal proposal)
        {
            // Lógica compleja de matching de reglas
            return rule.MinAmount <= proposal.EstimatedAmount &&
                   (rule.MaxAmount >= proposal.EstimatedAmount || rule.MaxAmount == 0) &&
                   (rule.Area == null || rule.Area == proposal.Area) &&
                   (rule.type == null || rule.type == proposal.type);
        }
    }
}
