using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.commands.Proposals;
using Aplicacion.DTOs;
using Aplicacion.queries.handlers;
using Aplicacion.queries;
using Aplicacion.services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aplicacion
{
    public static class DepencenciInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IRequestHandler<CreateProposalCommand, int>, CreateProposalHandler>();
            services.AddScoped<IRequestHandler<ProcessApprovalCommand, bool>, ProcessApprovalHandler>();

            // Queries
            services.AddScoped<IRequestHandler<GetProposalStatusQuery, string>, GetProposalStatusHandler>();
            services.AddScoped<IRequestHandler<GetProposalByIdQuery, ProposalDto>, GetProposalByIdHandler>();
            services.AddScoped<IRequestHandler<GetAllProposalsQuery, List<ProposalSummaryDto>>, GetAllProposalsHandler>();
            services.AddScoped<IRequestHandler<GetPendingStepsByRoleQuery, List<PendingStepDto>>, GetPendingStepsByRoleHandler>();

            // Services
            services.AddScoped<IApprovalWorkflowService, ApprovalWorkflowService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
