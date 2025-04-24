using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.exeptions;
using Dominio.repositorios;
using MediatR;

namespace Aplicacion.commands.Proposals
{
    class ProcessApprovalHandler : IRequestHandler<ProcessApprovalCommand, bool>
    {
        private readonly IProjectApprovalStepRepository _stepRepo;
        private readonly IProjectProposalRepository _proposalRepo;
        private readonly IUserRepository _userRepo;

        public ProcessApprovalHandler(
            IProjectApprovalStepRepository stepRepo,
            IProjectProposalRepository proposalRepo,
            IUserRepository userRepo)
        {
            _stepRepo = stepRepo;
            _proposalRepo = proposalRepo;
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(ProcessApprovalCommand request, CancellationToken cancellationToken)
        {
            var step = await _stepRepo.ObtenerPorIdAsync(request.StepId);
            if (step == null)
                throw new InvalidApprovalOperationException("Paso de aprobación no encontrado");

            var proposal = await _proposalRepo.ObtenerPorIdAsync(step.ProjectProposalId);
            if (proposal == null)
                throw new InvalidApprovalOperationException("Proyecto no encontrado");

            var user = await _userRepo.ObtenerPorIdAsync(request.UserId);
            if (user == null)
                throw new InvalidApprovalOperationException("Usuario no encontrado");

            // Validar que el usuario tenga el rol requerido
            if (user.Role != step.ApproverRoleId)
                throw new InvalidApprovalOperationException("Usuario no autorizado para esta aprobación");

            // Validar que el paso esté pendiente
            if (step.Status != 1) // 1 = Pendiente
                throw new InvalidApprovalOperationException("El paso ya fue procesado");

            // Validar orden de pasos
            var allSteps = await _stepRepo.ObtenerPorProyectoIdAsync(proposal.Id);
            var previousSteps = allSteps.Where(s => s.StepOrder < step.StepOrder);
            if (previousSteps.Any(s => s.Status != 2)) // 2 = Aprobado
                throw new InvalidApprovalOperationException("Deben aprobarse los pasos anteriores primero");

            // Actualizar el paso
            step.Status = request.Status;
            step.Observations = request.Observations;
            step.ApproverUserId = request.UserId;
            step.DecisionDate = DateTime.UtcNow;

            await _stepRepo.ActualizarAsync(step);

            // Actualizar estado del proyecto si es necesario
            if (request.Status == 3) // 3 = Rechazado
            {
                proposal.Status = 3; // Rechazado
                await _proposalRepo.ActualizarAsync(proposal);
            }
            else if (request.Status == 2) // 2 = Aprobado
            {
                // Verificar si es el último paso
                var isLastStep = !allSteps.Any(s => s.StepOrder > step.StepOrder && s.Status != 2);
                if (isLastStep)
                {
                    proposal.Status = 2; // Aprobado
                    await _proposalRepo.ActualizarAsync(proposal);
                }
            }

            return true;
        }
    }
}

