using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.repositorios;
using Infraestructura.datos;
using Infraestructura.repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registro de repositorios
            services.AddScoped<IApproverRoleRepository, ApproverRoleRepository>();
            services.AddScoped<IApprovalStatusRepository, ApprovalStatusRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApprovalRuleRepository, ApprovalRuleRepository>();
            services.AddScoped<IProjectProposalRepository, ProjectProposalRepository>();
            services.AddScoped<IProjectApprovalStepRepository, ProjectApprovalStepRepository>();

            return services;
        }
    }
}
