using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.entidades;

namespace Aplicacion.services
{
    interface IApprovalWorkflowService
    {
        Task GenerateApprovalWorkflow(ProjectProposal proposal);
    }
}
